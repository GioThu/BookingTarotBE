using AutoMapper;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using Service.Model.PostModel;
using Service.Model.UserModel;
using TarotBooking.Mappers;
using TarotBooking.Model.ImageModel;
using TarotBooking.Model.PostModel;
using TarotBooking.Model.PostModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;
        private readonly IReaderRepo _readerRepo;
        private readonly IUserRepo _userRepo;
        private readonly IImageService _imageService;


        public PostService(IPostRepo postRepo, IMapper mapper, IUserRepo userRepo, IReaderRepo readerRepo, IImageService imageService)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _readerRepo = readerRepo;
            _userRepo = userRepo;
            _imageService = imageService;
        }


        public async Task<bool> DeletePost(string postId)
        {
            var post = await _postRepo.GetById(postId);

            if (post == null) throw new Exception("Unable to find post!");

            post.Status = "Blocked";

            return await _postRepo.Update(post) != null;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var post = await _postRepo.GetAll();

            if (post == null) throw new Exception("No post in stock!");

            return post.ToList();
        }

        public async Task<Post?> UpdatePost(UpdatePostModel updatePostModel)
        {
            var post = await _postRepo.GetById(updatePostModel.Id);

            if (post == null) throw new Exception("Unable to find post!");

            var updatePost = updatePostModel.ToUpdatePost();
            
            if (updatePost == null) throw new Exception("Unable to update post!");
            var newPost = await _postRepo.Update(updatePost);
            if (updatePostModel.Image != null)
            {
                var createImageModel = new CreateImageModel
                {
                    PostId = updatePostModel.Id,
                    File = updatePostModel.Image
                };

                var image= await _imageService.CreateImageAndDeleteOldImages(createImageModel);

            }
            return newPost;
        }

        public async Task<PostWithImageModel?> GetPostWithImageById(string postId)
        {
            var post = await _postRepo.GetById(postId);
            if (post == null) throw new Exception("Post not found!");

            // Lấy hình ảnh mới nhất
            var images = await _postRepo.GetPostImagesById(postId);
            var latestImageUrl = images.OrderByDescending(img => img.CreateAt).FirstOrDefault()?.Url; 

            return new PostWithImageModel
            {
                post = _mapper.Map<PostDto>(post),
                url = latestImageUrl // Chỉ lấy hình ảnh mới nhất
            };
        }


        public async Task<List<PostWithImageModel>> GetPagedPostsAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var posts = await _postRepo.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                posts = posts.Where(u => u.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            if (posts == null || !posts.Any()) throw new Exception("No posts found!");

            var pagedPosts = posts.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Select(post =>
                                  {
                                      var images = _postRepo.GetPostImagesById(post.Id).Result; // Lấy hình ảnh
                                      var latestImageUrl = images.OrderByDescending(img => img.CreateAt).FirstOrDefault()?.Url; // Lấy hình ảnh mới nhất

                                      return new PostWithImageModel
                                      {
                                          post = _mapper.Map<PostDto>(post),
                                          url = latestImageUrl // Chỉ lấy hình ảnh mới nhất
                                      };
                                  })
                                  .ToList();

            return pagedPosts;
        }

        public async Task<PagedPostModel> GetPagedPostsNew(int pageNumber, int pageSize, string searchTerm)
        {
            var posts = await _postRepo.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                posts = posts.Where(u => u.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            if (posts == null || !posts.Any()) throw new Exception("No posts found!");

            // Calculate TotalItems and TotalPages
            var totalItems = posts.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedPosts = posts.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Select(post =>
                                  {
                                      var images = _postRepo.GetPostImagesById(post.Id).Result;
                                      var latestImageUrl = images.OrderByDescending(img => img.CreateAt).FirstOrDefault()?.Url;

                                      return new PostWithImageModel
                                      {
                                          post = _mapper.Map<PostDto>(post),
                                          url = latestImageUrl
                                      };
                                  })
                                  .ToList();

            return new PagedPostModel
            {
                Posts = pagedPosts,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<int> GetPostCountByReaderId(string readerId)
        {
            return await _postRepo.GetPostCountByReaderId(readerId);
        }

        public async Task<PagedPostModel> GetPagedPostsByReaderIdAsync(string readerId, int pageNumber, int pageSize)
        {
            var posts = await _postRepo.GetAll();

            // Filter posts by ReaderId
            var filteredPosts = posts.Where(post => post.ReaderId == readerId).ToList();

            if (!filteredPosts.Any()) throw new Exception("No posts found for the specified reader!");

            var totalItems = filteredPosts.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedPosts = filteredPosts.Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .Select(post =>
                                          {
                                              var images = _postRepo.GetPostImagesById(post.Id).Result;
                                              var latestImageUrl = images.OrderByDescending(img => img.CreateAt).FirstOrDefault()?.Url;

                                              return new PostWithImageModel
                                              {
                                                  post = _mapper.Map<PostDto>(post),
                                                  url = latestImageUrl
                                              };
                                          })
                                          .ToList();

            return new PagedPostModel
            {
                Posts = pagedPosts,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }


        public async Task<Post?> CreatePost(CreatePostModel createPostModel)
        {
            // Kiểm tra createPostModel
            if (createPostModel == null)
            {
                throw new ArgumentNullException(nameof(createPostModel), "Post data cannot be null.");
            }

            // Tạo Post từ model
            var createPost = createPostModel.ToCreatePost();
            if (createPost == null)
            {
                throw new Exception("Unable to map CreatePostModel to Post.");
            }

            // Lưu Post vào cơ sở dữ liệu
            var newPost = await _postRepo.Add(createPost);
            if (newPost == null)
            {
                throw new Exception("Unable to create post!");
            }

            if (createPostModel.Image != null)
            {
                var createImageModel = new CreateImageModel
                {
                    PostId = newPost.Id,
                    File = createPostModel.Image
                };
                await _imageService.CreateImage(createImageModel);
            }
            return newPost;
        }


        public async Task<PostDetail> GetPostDetailByIdAsync(string postId)
        {
            var post = await _postRepo.GetById(postId);

            if (post == null) throw new Exception("Post not found!");

            string name;
            if (post.ReaderId != null)
            {
                var reader = await _readerRepo.GetById(post.ReaderId);
                name = reader?.Name ?? "Unknown Reader";
            }
            else
            {
                var user = await _userRepo.GetById(post.UserId);
                name = user?.Name ?? "Unknown User";
            }

            var images = await _postRepo.GetPostImagesById(postId);
            var imageUrls = images.OrderByDescending(img => img.CreateAt)
                                  .Select(img => img.Url)
                                  .ToList();

            return new PostDetail
            {
                Name = name,
                Post = _mapper.Map<PostDto>(post),
                Url = imageUrls
            };
        }



    }
}
