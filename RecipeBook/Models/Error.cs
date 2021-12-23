﻿using Newtonsoft.Json;

namespace RecipeBook.Models
{
    public class Error
    {
        public int StatusCode;
        public string Message;
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
