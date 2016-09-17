
function Vec3(_x, _y, _z)
{
    var x = 0, y = 0, z = 0;

    this.__defineGetter__("X", function() {
        return x;
    });
    this.__defineSetter__("X", function(val) {
        if (isNaN(val)) throw new Error("Value is NaN");
        x = val;
    });

    this.__defineGetter__("Y", function() {
        return y;
    });
    this.__defineSetter__("Y", function(val) {
        if (isNaN(val)) throw new Error("Value is NaN");
        y = val;
    });

    this.__defineGetter__("Z", function() {
        return z;
    });
    this.__defineSetter__("Z", function(val) {
        if (isNaN(val)) throw new Error("Value is NaN");
        z = val;
    });

    if (typeof _x === "undefined") _x = 0;
    if (typeof _y === "undefined") _y = 0;
    if (typeof _z === "undefined") _z = 0;

    this.X = _x;
    this.Y = _y;
    this.Z = _z;


    this.add = function(vec)
    {
        return new Vec3(x + vec.X, y + vec.Y, z + vec.Z);
    }
    this.sub = function(vec)
    {
        return new Vec3(x - vec.X, y - vec.Y, z - vec.Z);
    }
    this.dot = function(vec)
    {
        return x*vec.X + y*vec.Y + z*vec.Z;
    }
    this.cross = function(vec)
    {
        return new Vec3(this.Y*vec.Z - this.Z*vec.Y, this.Z*vec.X - this.X*vec.Z, this.X*vec.Y - this.Y*vec.X);
    }
    this.length = function()
    {
        return Math.sqrt(x*x + y*y + z*z);
    }
    this.scale = function(s)
    {
        return new Vec3(x*s, y*s, z*s);
    }
    this.normalize = function()
    {
        return this.scale(1 / this.length());
    }

    this.__defineGetter__("t", function()
    {
        return '{"X":'+x+', "Y":'+y+', "Z":'+z+'}'
    });
}