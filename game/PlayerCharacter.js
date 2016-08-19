

function PlayerCharacter()
{
    this.Name = "Noname";

    this.Stats = [1, 1, 1, 1];
    this.Skills = [1, 1, 1, 1, 1, 1, 1, 1];

    this.Stats.Names = ['Strength', 'Agility', 'Intelligence', 'Metabolism'];
    for (var i = 0; i < this.Stats.Names.length; i++)
    {
        this.Stats.__defineGetter__(this.Stats.Names[i], function() { return this.Stats[i]; });
    }

    this.Skills.Names = ['Crafting', 'Archery', 'Light Weapons', 'Guns', 'Cooking', 'Sneaking', 'Running', 'Climbing'];
    for (var i = 0; i < this.Skills.Names.length; i++)
    {
        this.Skills.__defineGetter__(this.Skills.Names[i], function() { return this.Skills[i]; });
    }

    this.Stats.__defineGetter__('FreePoints', function() {
        var sum = 0;
        for (var i = 0; i < this.length; i++)
            sum += this[i];
        return 19 - sum;
    })
    this.Skills.__defineGetter__('FreePoints', function() {
        var sum = 0;
        for (var i = 0; i < this.length; i++)
            sum += this[i];
        return 23 - sum;
    })

    this.Randomize = function()
    {
        for (var i = 0; i < this.Stats.length; i++) {
            this.Stats[i] = 1;
        }
        for (var i = 0; i < this.Skills.length; i++) {
            this.Skills[i] = 1;
        }

        while (this.Stats.FreePoints > 0)
        {
            var dice = Math.floor(Math.random() * this.Stats.length);
            this.Stats[dice]++;
        }
        while (this.Skills.FreePoints > 0)
        {
            var dice = Math.floor(Math.random() * this.Skills.length);
            this.Skills[dice]++;
        }
    }

    this.toString = function()
    {
        var stat_names = [ "Strong", "Agile", "Inteligent", "Undemanding" ];
        var skill_names = [ "Craftsman", "Archer", "Swordsman", "Gunman", "Cook", "Sneak", "Runner", "Climber" ];

        var max_stat_index = this.Stats.indexOf(Math.max.apply(Math, this.Stats));
        var max_skill_index = this.Skills.indexOf(Math.max.apply(Math, this.Skills));

        return stat_names[max_stat_index] + " " + skill_names[max_skill_index];
    }

    this.Import = function(chr)
    {
        for (var i = 0; i < this.Stats.length; i++) {
            this.Stats[i] = chr.Stats[i]
        }
        for (var i = 0; i < this.Skills.length; i++) {
            this.Skills[i] = chr.Skills[i]
        }

        this.Name = chr.Name;
    }
}




