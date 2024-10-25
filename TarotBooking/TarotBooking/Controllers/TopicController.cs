using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.TopicModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicWebController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicWebController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        [Route("topics-list")]
        public async Task<List<Topic>> GetAllTopics()
        {
            return await _topicService.GetAllTopics();
        }


        [HttpPost]
        [Route("create-topic")]
        public async Task<Topic?> CreateTopic([FromForm] CreateTopicModel createTopicModel)
        {
            return await _topicService.CreateTopic(createTopicModel);
        }

        [HttpPost]
        [Route("update-topic")]
        public async Task<Topic?> UpdateTopic([FromForm] UpdateTopicModel updateTopicModel)
        {
            return await _topicService.UpdateTopic(updateTopicModel);
        }

        [HttpPost]
        [Route("delete-topic")]
        public async Task<bool> DeleteTopic([FromForm] Topic topic)
        {
            return await _topicService.DeleteTopic(topic.Id);
        }



    }
}
