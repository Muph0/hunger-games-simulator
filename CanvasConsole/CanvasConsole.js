
window.CanvasConsole = function(width, height, img) {

    // Constants & aliases
    var ASCII = "\0☺☻♥♦♣♠•◘○\n♂♀\r♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼ !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~⌂ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥₧ƒáíóúñÑªº¿⌐¬½¼¡«»░▒▓│┤╡╢╖╕╣║╗╝╜╛┐└┴┬├─┼╞╟╚╔╩╦╠═╬╧╨╤╥╙╘╒╓╫╪┘┌█▄▌▐▀αßΓπΣσµτΦΘΩδ∞φε∩≡±≥≤⌠⌡÷≈°∙·√ⁿ²■ ";
    var CHAR_WIDTH = 7, CHAR_HEIGHT = 12;
    var self = this;

    ////////////////////
    // private sector
    var rgb = function(color) { return 'rgb(' + color.slice(0,3).join(',') + ')'; }
    var defaultFor = function(variable, value) { return typeof variable !== 'undefined' ? variable : value; }

    var width = defaultFor(width, 80);
    var height = defaultFor(height, 25);

    var canvas = null;
    var ctx = null

    var font_spritesheet = null;

    var constructor = function() {
        if (img instanceof HTMLElement) self.LoadFont(img, function() {});
    }

    ////////////////////
    //  Public sector
    self.Foreground = [0, 200, 200];
    self.Background = [0, 0, 0];

    self.CursorX = 0;
    self.CursorY = 0;

    self.SetCursor = function(x, y)
    {
        if (x instanceof Array && typeof y === 'undefined')
        {
            self.CursorX = defaultFor(x[0], self.CursorX);
            self.CursorY = defaultFor(x[1], self.CursorY);
        }
        else
        {
            self.CursorX = defaultFor(x, self.CursorX);
            self.CursorY = defaultFor(y, self.CursorY);
        }
    }
    self.GetCursor = function()
    {
        return [self.CursorX, self.CursorY];
    }

    self.GetWidth = function() { return width; }
    self.GetHeight = function() { return height; }

    self.LoadFont = function(path_or_img, onload_callback) {

        var ascii_charset = null;
        var onload = function()
        {
            // we need to replace black pixels with transparent ones
            var cvs = document.createElement('canvas');

            // resize the temporary canvas according to the spritesheet
            cvs.width = ascii_charset.width;
            cvs.height = ascii_charset.height;

            // get the canvas context & draw the image in it
            var charctx = cvs.getContext('2d');
            charctx.drawImage(ascii_charset,0,0);

            var imgData = charctx.getImageData(0, 0, cvs.width, cvs.height);
            for (var i = 0; i < cvs.width * cvs.height * 4; i += 4)
            {
                if (imgData.data[i] == 0 && imgData.data[i+1] == 0 && imgData.data[i+2] == 0)
                    imgData.data[i+3] = 0;
            }
            charctx.putImageData(imgData, 0, 0);

            font_spritesheet = cvs;
            onload_callback();
        }

        if (path_or_img.nodeName === "IMG")
        {
            ascii_charset = path_or_img;
            onload();
        }
        else if (typeof path_or_img === 'string')
        {
            ascii_charset = new Image();
            ascii_charset.onload = onload;
            ascii_charset.src = path_or_img;
        }
        else
        {
            throw new TypeError('CanvasConsole.LoadFont: wrong argument 1' +
                ' - expected instance of HTMLImageElement or string instead of '
                + (typeof path_or_img) + '.');
        }
    }

    self.CreateCanvas = function(parentElement) {

        // Create the canvas
        canvas = document.createElement('canvas');
        // set the size
        canvas.width = width * CHAR_WIDTH;
        canvas.height = height * CHAR_HEIGHT;
        // set background
        canvas.style.backgroundColor = 'darkblue';

        // Get the context
        ctx = canvas.getContext('2d');
        ctx.imageSmoothingEnabled = false;

        if (document.body === null)
            throw new Error('document.body is null - you should call this method later.')

        if (parentElement !== null)
        {
            parentElement = defaultFor(parentElement, document.body);
            parentElement.appendChild(canvas);
        }
        self.Clear();
    }

    self.DrawChar = function(chr, cx, cy)
    {
        var index = ASCII.indexOf(chr);

        // calc the source location in the font spritesheet
        var x = (index % 64) * CHAR_WIDTH;
        var y = Math.floor(index / 64) * CHAR_HEIGHT;

        // calc the destination location on the canvas
        var X = cx * CHAR_WIDTH;
        var Y = cy * CHAR_HEIGHT;

        // Draw the character MASK in empty rectangle
        ctx.clearRect(X,Y, CHAR_WIDTH, CHAR_HEIGHT);
        ctx.drawImage(font_spritesheet, x, y, CHAR_WIDTH, CHAR_HEIGHT, X, Y, CHAR_WIDTH, CHAR_HEIGHT);

        // Draw fg color on top of the mask
        ctx.globalCompositeOperation="source-atop";
        ctx.fillStyle = rgb(self.Foreground);
        ctx.fillRect(X, Y, CHAR_WIDTH, CHAR_HEIGHT);

        // fill the rest with bg color
        ctx.globalCompositeOperation="destination-over";
        ctx.fillStyle = rgb(self.Background);
        ctx.fillRect(X, Y, CHAR_WIDTH, CHAR_HEIGHT);

        // reset globalCompositeOperation to normal
        ctx.globalCompositeOperation="source-over";
    }

    self.Write = function(obj)
    {
        if (obj === null) return;
        var str = obj.toString();

        if (str.length == 1)
        {
            switch (str)
            {
                case '\n':
                    // if LF, move to next line
                    self.CursorX = 0;
                    self.CursorY++;
                    break;

                case '\t':
                    var tab_size = 4;
                    var dif = tab_size - (self.CursorX % tab_size);
                    var s = new Array(dif + 1).join(' ');
                    self.Write(s);
                    break;

                default:
                    self.DrawChar(str, self.CursorX, self.CursorY);
                    self.CursorX++;
            }

            // if we step over the end of line
            if (self.CursorX >= self.width)
            {
                // go to the next line
                self.CursorX = 0;
                self.CursorY++;
            }

            // if we get over the last line
            if (self.CursorY >= self.height)
            {
                // move everythin one line up
                self.ctx.drawImage(self.canvas, 0, -CHAR_HEIGHT);
                // clear last line
                self.ctx.fillStyle = rgb(self.Background);
                self.ctx.fillRect(0, (self.height - 1) * CHAR_HEIGHT, self.width * CHAR_WIDTH, CHAR_HEIGHT);
                // move the cursor back to the last line
                self.CursorY = height - 1;
            }
        }
        if (str.length > 1)
        {
            str.split('').forEach(self.Write);
        }
    }

    self.WriteLine = function(obj)
    {
        self.Write(obj);
        self.Write('\n');
    }

    self.Clear = function()
    {
        ctx.fillStyle = rgb(self.Background);
        ctx.fillRect(0, 0, width * CHAR_WIDTH, height * CHAR_HEIGHT)
        self.SetCursor(0,0);
    }
    self.ClearRectangle = function(rect_width, rect_height)
    {
        ctx.fillStyle = rgb(self.Background);
        ctx.fillRect(self.CursorX * CHAR_WIDTH, self.CursorY * CHAR_WIDTH, width * CHAR_WIDTH, height * CHAR_HEIGHT)
    }

    self.WriteImage = function(img)
    {
        ctx.drawImage(img, self.CursorX * CHAR_WIDTH, self.CursorY * CHAR_WIDTH);
    }
    self.GetCanvas = function()
    {
        return canvas;
    }

constructor(); }