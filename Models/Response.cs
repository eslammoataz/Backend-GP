﻿namespace WebApplication1.Models
{
    public class Response<T>
    {
        public bool isError { get; set; }= false;
        
        public string Message { get; set; } = "";
        public List<string> Errors { get; set; } = new List<string>();
        
        public T? Payload { get; set; }
        
        

    }
}
