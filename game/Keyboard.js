
function Keyboard()
{
    var self = this;

    self.Keys = { Backspace: 8, Tab: 9, Enter: 13, Escape: 27, Spacebar: 32, PageUp: 33, PageDown: 34,  End: 35, Home: 36, Left: 37, Up: 38, Right: 39, Down: 40, Delete: 46, Numpad0: 96, Numpad1: 97, Numpad2: 98, Numpad3: 99, Numpad4: 100, Numpad5: 101, Numpad6: 102, Numpad7: 103, Numpad8: 104, Numpad9: 105, NumpadMultiply: 106, NumpadAdd: 107, NumpadEnter: 108, NumpadSubtract: 109, NumpadDecimal: 110, NumpadDivide: 111, Comma: 188, Period: 190, };
    // Add letters to self.Keys enum
    for (var i = 65; i <= 90; i++)
    {
        self.Keys[String.fromCharCode(i)] = i;
    }

    self.Buffer = [];

    // create reversed dictionary
    var keys_reverted = {}
    for (var key in self.Keys)
    {
        var value = self.Keys[key];
        keys_reverted[value] = key;
    }

    // fill the keyboard state with false
    var keyboard_state = [];
    for (var key in self.Keys)
    {
        keyboard_state[self.Keys[key]] = false;
    }

    self.keyup = function(event)
    {
        keyboard_state[event.keyCode] = false;
    }
    self.keydown = function(event)
    {
        keyboard_state[event.keyCode] = true;

        if (event.keyCode === self.Keys.Backspace)
        {
            self.Buffer.pop();
        }
    }
    self.keypress = function(event)
    {
        self.Buffer.push(event.key);
    }

    self.GetState = function()
    {
        return keyboard_state.slice(0);
    }

}
window.Keyboard = new Keyboard();