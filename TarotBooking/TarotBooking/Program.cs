using Google.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Services;
using System.Text;
using TarotBooking.Mapping;

using TarotBooking.Models;
using TarotBooking.Repositories.Implementations;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Implementations;
using TarotBooking.Services.Interfaces;
namespace TarotBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<JwtService>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IReaderService, ReaderService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<IGroupCardService, GroupCardService>();
            builder.Services.AddScoped<IReaderTopicService, ReaderTopicService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IFollowService, FollowService>();
            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<PaymentService>();   

            builder.Services.AddScoped<ICommentRepo, CommentRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IReaderRepo, ReaderRepo>();
            builder.Services.AddScoped<IPostRepo, PostRepo>();
            builder.Services.AddScoped<IImageRepo, ImageRepo>();
            builder.Services.AddScoped<INotificationRepo, NotificationRepo>();
            builder.Services.AddScoped<ITopicRepo, TopicRepo>();
            builder.Services.AddScoped<IGroupCardRepo, GroupCardRepo>();
            builder.Services.AddScoped<IUserGroupCardRepo, UserGroupCardRepo>();
            builder.Services.AddScoped<IReaderTopicRepo, ReaderTopicRepo>();
            builder.Services.AddScoped<IBookingTopicRepo, BookingTopicRepo>();
            builder.Services.AddScoped<IBookingRepo, BookingRepo>();
            builder.Services.AddScoped<IFollowRepo, FollowRepo>();
            builder.Services.AddScoped<ICardRepo, CardRepo>();


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<FirebaseService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));



            //builder.Services.AddScoped<IAuthService, AuthService>();


            //// Add services to the container.

            //// Register the Basic Authentication Scheme
            //builder.Services.AddAuthentication("BasicAuthentication")
            //    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHander>("BasicAuthentication", null);

            //// Add Authorization Policy
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
            //});

            builder.Services.AddAuthentication(options =>
            {

                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
                    ValidAudience = builder.Configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SecretKey"]))
                };
                options.SaveToken = true;
            })
            .AddCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None; // Set SameSite to None
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Set SecurePolicy to Always
                options.Cookie.HttpOnly = true; // Optionally, set HttpOnly for security
            })
            .AddGoogle(googleOptions =>
            {
                IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                googleOptions.Events.OnRemoteFailure = context =>
                {
                    context.Response.Redirect("/error?failureMessage=" + context.Failure.Message);
                    context.HandleResponse();
                    return Task.CompletedTask;
                };
            });

            // Add DB context
            builder.Services.AddDbContext<BookingTarotContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookingTarot")));


            builder.Services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                 });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "jwtToken_Auth_API",
                    Version = "v1",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Token with bearer format here bearer[space]token"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference= new OpenApiReference
                                {
                                    Type= ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                });
            });



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://demo-tarot-webapp-7cpb.vercel.app", "https://www.bookingtarot.somee.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowAllOrigins");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
