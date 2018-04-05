###Overview
Develop a computer game where two heroes fight each other in the arena.

####Game rules
The game goes in turns. The two heroes fight each other in turns. Hero 1 attacks hero 2. If hero 2 survives the attack, he attacks back. The game continues until one of the hero dies.

####Attack mechanism
The hero who attacks unleashes pure damage, measured in points. The hero who is being attacked can take some or all of the damage, depending on his skills.

####Specifications
All heroes have attributes:
- Health points – when health points go zero or lower, the hero is dead.
-  Attack points
- Armor points

When attacking, all heroes do randomly between 80% and 120% of their attack points as raw damage.
When defending, all heroes take damage, reduced randomly with between 80% and 120% of their armor points. The actual damage received reduces their health points. When the health points become zero or less, the hero is dead.

There are at least two types of heroes:
- Warrior
- Knight:
	* When defending, has a 20% chance to completely block the attack and receive no
damage.
	* When attacking, has a 10% chance to do 200% damage.
- Assassin:
	* When attacking, has a 30% chance to do 300% damage.
- Monk:
	* When defending, has a 30% chance to avoid the attack and receive no damage.
- Elf
	* When defending, has a 10% chance to completely block the attack and receive no damage.
	* When attacking, has a 10% chance to do 150% damage.
- Teenager
	* When attacking, has a 20% chance to do 200% damage.

###[Demo](https://drive.google.com/file/d/1Zxsknb92yI_Y11rRBqaNlE7_5YRzw0zl/view?usp=sharing)
