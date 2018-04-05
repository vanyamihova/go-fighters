"use strict";

var fighter;
var opponent;

$(document).ready(function(){

    // Fetch canvas elements, if they exists
    var canvasFighter = null;
    var canvasOpponent = null;

    setTimeout(function() {
        canvasFighter = document.getElementById('fighter');
        canvasOpponent = document.getElementById('opponent');
    
        if (canvasFighter != null) {
            var fighterType = canvasFighter.attributes["hero-type"].nodeValue;
            fighter = new SpriteSheet(canvasFighter, '/images/moves/moves-' + fighterType + '.png', 64, 64, 9);
            fighter.idle();
            fighter.animate();

            var opponentType = canvasOpponent.attributes["hero-type"].nodeValue;
            opponent = new SpriteSheet(canvasOpponent, '/images/moves/moves-' + opponentType + '.png', 64, 64, 9);
            opponent.idle();
            opponent.animate();
        }
    }, 500);

    // FUNCTIONS 
    function SpriteSheet(canvas, path, frameWidth, frameHeight, frameSpeed) {
        var
            ctx = canvas.getContext('2d'),
            image = new Image(),
            _this = this,
            framesPerRow = 0,
            animationSequence = [],  // array holding the order of the animation
            currentFrame = 0,        // the current frame to draw
            counter = 0;             // keep track of frame rate

 
        // calculate the number of frames in a row after the image loads
        image.onload = function() {
            framesPerRow = Math.floor(image.width / frameWidth);
        };
        image.src = path;

        // Stabbling
        $(document).on(canvas.id + "-stabbling", function(event, arg1, arg2) {
            _this.setup(0, 5);
            reset();
        });

        // Victory
        $(document).on(canvas.id + "-victory", function(event, arg1, arg2) {
            _this.setup(15, 17);
        });

        // Defend
        $(document).on(canvas.id + "-defend", function(event, arg1, arg2) {
            _this.setup(51, 53);
        });

        // Guard
        $(document).on(canvas.id + "-guard", function(event, arg1, arg2) {
            _this.setup(27, 29);
            reset();
        });

        // Damage
        $(document).on(canvas.id + "-guard-with-damage", function(event, arg1, arg2) {
            _this.setup(27, 29);
            setTimeout(function() {
                _this.setup(36, 38);
                reset();
            }, 500);
        });

        function reset() {
            setTimeout(function() { _this.idle(); }, 1000);
        }

        _this.setup = function(startFrame, endFrame) {
            animationSequence = [];
            // start and end range for frames
            for (var frameNumber = startFrame; frameNumber <= endFrame; frameNumber++) {
                animationSequence.push(frameNumber);
            }
        }

        _this.idle = function() {
            _this.setup(0, 2);
        }

        /**
         * Update the animation
         */
        _this.update = function() {
            // update to the next frame if it is time
            if (counter == (frameSpeed - 1)) {
                currentFrame = (currentFrame + 1) % animationSequence.length;
            }

            // update the counter
            counter = (counter + 1) % frameSpeed;
        };

        /**
         * Draw the current frame
         * @param {integer} x - X position to draw
         * @param {integer} y - Y position to draw
         */
        _this.draw = function(x, y) {
            // get the row and col of the frame
            var frameValue = animationSequence[currentFrame];
            var row = Math.floor(frameValue / framesPerRow);
            var col = Math.floor(frameValue % framesPerRow);

            ctx.drawImage(
                image,
                col * frameWidth, row * frameHeight,
                frameWidth, frameHeight,
                x, y,
                frameWidth, frameHeight);
        };

        _this.clear = function() {
            ctx.clearRect(0, 0, 150, 150);
        };

        _this.animate = function() {
            requestAnimationFrame(_this.animate);
            _this.clear();
            _this.update();
            _this.draw(12.5, 12.5);
        }

    }

});
