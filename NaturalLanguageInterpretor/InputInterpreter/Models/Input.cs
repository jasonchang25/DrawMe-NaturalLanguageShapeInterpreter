using System;
using System.Collections.Generic;

namespace InputInterpreter.Models
{
    // Input class created so api calls are pass by data instead of header keys, helps with lengthly string due to 255character limit
    public class ShapeInput
    {
        public string Input { get; set; }
    }
}