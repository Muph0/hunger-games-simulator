
/**
 * @constructor
 */
function Vec3(_x, _y, _z)
{
    //var x = 0, y = 0, z = 0;

    /*this.__ defineGetter__("X", function() {
        return x;
    });
    this.__ defineSetter__("X", function(val) {
        if (isNaN(val)) throw new Error("Value is NaN");
        x = val;
    });

    this.__ defineGetter__("Y", function() {
        return y;
    });
    this.__ defineSetter__("Y", function(val) {
        if (isNaN(val)) throw new Error("Value is NaN");
        y = val;
    });

    this.__ defineGetter__("Z", function() {
        return z;
    });
    this.__ defineSetter__("Z", function(val) {
        if (isNaN(val)) throw new Error("Value is NaN");
        z = val;
    });*/

    if (typeof _x === "undefined") _x = 0;
    if (typeof _y === "undefined") _y = 0;
    if (typeof _z === "undefined") _z = 0;

    this.X = _x;
    this.Y = _y;
    this.Z = _z;


    this.add = function(vec)
    {
        return new Vec3(this.X + vec.X, this.Y + vec.Y, this.Z + vec.Z);
    }
    this.sub = function(vec)
    {
        return new Vec3(this.X - vec.X, this.Y - vec.Y, this.Z - vec.Z);
    }
    this.dot = function(vec)
    {
        return this.X*vec.X + this.Y*vec.Y + this.Z*vec.Z;
    }
    this.cross = function(vec)
    {
        return new Vec3(this.Y*vec.Z - this.Z*vec.Y, this.Z*vec.X - this.X*vec.Z, this.X*vec.Y - this.Y*vec.X);
    }
    this.length = function()
    {
        return Math.sqrt(this.X*this.X + this.Y*this.Y + this.Z*this.Z);
    }
    this.scale = function(s)
    {
        return new Vec3(this.X*s, this.Y*s, this.Z*s);
    }
    this.normalize = function()
    {
        return this.scale(1 / this.length());
    }

    this.t = function()
    {
        return '{"X":'+this.X+', "Y":'+this.Y+', "Z":'+this.Z+'}'
    };
}