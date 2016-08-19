
function Keyboard()
{
    this.Keys = { Backspace: 8, Tab: 9, Enter: 13, Escape: 27, Spacebar: 32, PageUp: 33, PageDown: 34,  End: 35, Home: 36, Left: 37, Up: 38, Right: 39, Down: 40, Delete: 46, Numpad0: 96, Numpad1: 97, Numpad2: 98, Numpad3: 99, Numpad4: 100, Numpad5: 101, Numpad6: 102, Numpad7: 103, Numpad8: 104, Numpad9: 105, NumpadMultiply: 106, NumpadAdd: 107, NumpadEnter: 108, NumpadSubtract: 109, NumpadDecimal: 110, NumpadDivide: 111, Comma: 188, Period: 190, };
    // Add letters to this.Keys enum
    for (var i = 65; i <= 90; i++)
    {
        this.Keys[String.fromCharCode(i)] = i;
    }

    this.Buffer = [];

    // create reversed dictionary
    var keys_reverted = {}
    for (var key in this.Keys)
    {
        var value = this.Keys[key];
        keys_reverted[value] = key;
    }

    // fill the keyboard state with false
    var keyboard_state = [];
    for (var key in this.Keys)
    {
        keyboard_state[this.Keys[key]] = false;
    }

    this.keyup = function(event)
    {
        keyboard_state[event.keyCode] = false;
    }
    this.keydown = function(event)
    {
        keyboard_state[event.keyCode] = true;
    }
    this.keypress = function(event)
    {
        this.Buffer.push(event.key);
    }

    this.GetState = function()
    {
        return keyboard_state.slice(0);
    }

}
window.Keyboard = new Keyboard();