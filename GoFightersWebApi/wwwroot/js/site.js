"use strict";

var
    OK = "OK",
    CHOSEN_IS_ATTACKED = "CHOSEN_IS_ATTACKED",
    OPPONENT_IS_ATTACKED = "OPPONENT_IS_ATTACKED",
    CHOSEN_IS_WINNER = "CHOSEN_IS_WINNER",
    OPPONENT_IS_WINNER = "OPPONENT_IS_WINNER",
    UNKNOWN = "unknown";

var app = angular.module('GoFightersApp', ["ngRoute"]);

app.config(function($routeProvider) {
    $routeProvider
        .when("/", {
            controller: "ChooseHeroesController",
            controllerAs: "chooseHeroes",
            templateUrl : "../views/choose_hero_view.html"
        })
        .when("/fight", {
            controller: "FightController",
            controllerAs: "fight",
            templateUrl : "../views/fight_view.html"
        });
});

app.controller('GoFightersController', function($scope, $http) {
    var _this = this;

    _this.resetFight = function() {
        _this.fighters.chosenHero = {
            id: UNKNOWN,
            avatar: ""
        };
        _this.fighters.opponentHero = {
            id: UNKNOWN,
            avatar: ""
        };
    };

    _this.fighters = {};
    _this.resetFight();
});

app.controller('ChooseHeroesController', function($scope, $http, $location) {
    var _this = this;

    _this.step = 1;
    _this.fighters = $scope.$parent.goFighters.fighters;

    $http.get("api/heroes/")
        .then(function(response) {
            var response = response.data;

            if (response && response.status === OK) {
                _this.heroes = response.data;
            }
        });

    _this.chosedHero = function(heroId) {
        if (_this.step == 1) {
            _this.fighters.chosenHero.id = heroId;
            _this.step = 2;
            return;
        }

        if (_this.step == 2) {
            _this.fighters.opponentHero.id = heroId;
           _this.step = 3;
           return;
        }

        if (_this.step == 3) {
            $http.post("api/heroes", {
                    chosenId: _this.fighters.chosenHero.id,
                    opponentId: _this.fighters.opponentHero.id
                })
                .then(function(response) {
                    var response = response.data;

                    if (response && response.status === OK) {
                         $location.url('/fight');
                    }
                });
        }
    }
});

app.controller('FightController', function($scope, $http, $location, $timeout) {
    var _this = this;

    _this.buttonDisabled = false;
    _this.fighters = $scope.$parent.goFighters.fighters;

    $http.get("api/heroes/fighters")
        .then(function(response) {
            var response = response.data;

            if (response && response.status === OK && response.data[0] && response.data[1]) {
                _this.fighters.chosenHero = response.data[0];
                _this.fighters.opponentHero = response.data[1];
            }

            if (_this.fighters.chosenHero.id === UNKNOWN || _this.fighters.opponentHero.id === UNKNOWN) {
                $location.url("/");
                return;
            }

            if (response.status === CHOSEN_IS_WINNER) {
                chosenIsWinner();
            }

            if (response.status === OPPONENT_IS_WINNER) {
                opponentIsWinner();
            }
        });

    _this.healthProgressWidth = function(hero) {
        if (hero && hero.currentHealthPoints && hero.healthPoints) {
            return (hero.currentHealthPoints * 100 / hero.healthPoints);
        }
    };

    _this.attack = function() {
        _this.buttonDisabled = true;
        $timeout(function(){
            _this.buttonDisabled = false;
        }, 1500);

        $http.get("api/heroes/attack")
            .then(function(response) {
                var response = response.data;

                if (response.status === CHOSEN_IS_ATTACKED) {
                    attackActions("opponent", "fighter", response.data.damage);
                    _this.fighters.chosenHero.damage = response.data.damage;
                    _this.fighters.chosenHero.currentHealthPoints = response.data.healthPoints;
                    return;
                }

                if (response.status === OPPONENT_IS_ATTACKED) {
                    attackActions("fighter", "opponent", response.data.damage);
                    _this.fighters.opponentHero.damage = response.data.damage;
                    _this.fighters.opponentHero.currentHealthPoints = response.data.healthPoints;
                    return;
                }

                if (response.status === CHOSEN_IS_WINNER) {
                    $(document).trigger("fighter-victory");
                    $(document).trigger("opponent-defend");
                    _this.fighters.opponentHero.damage = response.data.damage;
                    heroIsDamaged("#opponent-damage");
                    chosenIsWinner();
                    return;
                }

                if (response.status === OPPONENT_IS_WINNER) {
                    $(document).trigger("opponent-victory");
                    $(document).trigger("fighter-defend");
                    _this.fighters.chosenHero.damage = response.data.damage;
                    heroIsDamaged("#fighter-damage");
                    opponentIsWinner();
                }
            });
    };

    _this.isFightCompleted = function() {
        return _this.fighters.chosenHero.currentHealthPoints === 0 ||
            _this.fighters.opponentHero.currentHealthPoints === 0;
    };

    _this.startNewFight = function() {
        $http.get("api/heroes/reset")
            .then(function(response) {
                var response = response.data;

                if (response && response.status === OK) {
                    $scope.$parent.goFighters.resetFight();
                    $location.url("/");
                }
            });
    };

    function attackActions(attacking, attacked, damage) {
        var guardEvent = attacked + ((damage === 0) ? "-guard" : "-guard-with-damage")
        $(document).trigger(attacking + "-stabbling");
        $(document).trigger(guardEvent);
        heroIsDamaged("#" + attacked + "-damage");
    }

    function opponentIsWinner() {
        _this.fighters.chosenHero.currentHealthPoints = 0;
        _this.fighters.opponentHero.avatar = "winner";
        _this.fighters.chosenHero.avatar = "defend";
    }

    function chosenIsWinner() {
        _this.fighters.opponentHero.currentHealthPoints = 0;
        _this.fighters.chosenHero.avatar = "winner";
        _this.fighters.opponentHero.avatar = "defend";
    }

    function heroIsDamaged(elementId) {
        var element = $(elementId);
        element.css("bottom", "129.844px");

        setTimeout(function() {
            element.show();
            element.animate({
                bottom: 159.844 + 'px'
            }, {
                duration: 500, 
                easing: "swing",
                complete: function() {
                    element.fadeOut("slow", function() {
                        element.hide();
                    });
                }
            });
        }, 500);
    }

});
