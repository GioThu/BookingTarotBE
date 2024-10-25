﻿using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using TarotBooking.Model.ImageModel;
using TarotBooking.Services.Implementations;
using TarotBooking.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageService _iImageService;

    public ImagesController(IImageService iImageService)
    {
        _iImageService =iImageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] CreateImageModel createImageModel)
    {
        if (createImageModel.File == null || createImageModel.File.Length == 0)
        {
            return BadRequest("File invalid");
        }

        var imageUrl = await _iImageService.CreateImage(createImageModel);

        return Ok(new { image = imageUrl });
    }


}