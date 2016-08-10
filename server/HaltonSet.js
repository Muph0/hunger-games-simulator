
function HaltonSet(seed)
{
    var k2, k3;

    var rnd = new Random(seed);
    k2 = 2 * rnd.Next(10000) + 1;
    k3 = 3 * rnd.Next(10000) + 1 + rnd.Next(2);


    this.Seq2 = function(i)
    {
        i *= k2;

        var b = 2;
        var r = 0; // double
        var f = 1; // double
        while (i > 0)
        {
            f /= b;
            r += f * (i % b);
            i = Math.floor(i / b);
        }
        return r;
    }
    this.Seq3 = function(i)
    {
        i *= k3;

        var b = 3;
        var r = 0; // double
        var f = 1; // double
        while (i > 0)
        {
            f /= b;
            r += f * (i % b);
            i = Math.floor(i / b);
        }
        return r;
    }
}