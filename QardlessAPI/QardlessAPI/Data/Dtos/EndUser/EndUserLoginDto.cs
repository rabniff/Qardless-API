﻿namespace QardlessAPI.Data.Dtos.EndUser
{
    public class EndUserLoginDto
    {
        public string Email { get; set; }

        public bool isLoggedin 
        {
            get { return isLoggedin; }
            
            set { isLoggedin = false; }
        }

    }
}