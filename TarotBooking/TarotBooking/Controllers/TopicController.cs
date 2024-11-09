using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.TopicModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.TopicModel;
using Microsoft.AspNetCore.Authorization;
using BE.Attributes;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicWebController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public TopicWebController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("topics-list")]
        public async Task<List<TopicDto>> GetAllTopics()
        {
            var topics = await _topicService.GetAllTopics();
            return _mapper.Map<List<TopicDto>>(topics);
        }


        [HttpPost]
        [Route("create-topic")]
        public async Task<TopicDto?> CreateTopic([FromForm] CreateTopicModel createTopicModel)
        {
            var topic = await _topicService.CreateTopic(createTopicModel);
            return _mapper.Map<TopicDto>(topic);
        }

        [HttpPost]
        [Route("update-topic")]
        public async Task<TopicDto?> UpdateTopic([FromForm] UpdateTopicModel updateTopicModel)
        {
            var topic = await _topicService.UpdateTopic(updateTopicModel);
            return _mapper.Map<TopicDto>(topic);
        }

        [HttpPost]
        [Route("delete-topic")]
        public async Task<bool> DeleteTopic(String topicId)
        {
            return await _topicService.DeleteTopic(topicId);
        }
    }
}
