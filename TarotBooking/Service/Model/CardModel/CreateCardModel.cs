﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.CardModel
{
    public class CreateCardModel
    {
        public string? GroupId { get; set; }
        public string? Element { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public IFormFile? img { get; set; }
    }
}
