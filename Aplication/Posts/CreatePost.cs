//vai ser usada para create novos posts

using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using Aplication.Interfaces;
using AutoMapper;
using Doiman;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts
{
    public class CreatePost
    {
        //recebe os dados que vem do mediator na classe PostController
        public class CreatePostCommand : IRequest<PostDto>
        {
            //os dados que eu quero armazenar
            public string Title { get; set; }
            public string Image { get; set; }
            public string Content { get; set; }//inicializo com a data atual
        }
        
        

        //classe para validarmos os dados que vem do da Classe CreatePostcommand
        public class CreatePostValidator : AbstractValidator<CreatePostCommand>
        {
            public CreatePostValidator()
            {
                RuleFor(x => x.Image).NotEmpty();
                RuleFor(x => x.Title).NotEmpty();
            }
        }

        //onde teremos a logica toda da criacao de um post
        public class CreatePostCommandHandle : IRequestHandler<CreatePostCommand, PostDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IPostRepository _postRepository;
            private readonly IUserAccessor _userAccessor;

            public CreatePostCommandHandle(DataContext context, IMapper mapper, UserManager<User> userManager,
                IPostRepository postRepository, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
                _postRepository = postRepository;
                _userAccessor = userAccessor;
            }

            public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                //validacao dos dados
                var postFound =
                    await _context.Post.FirstOrDefaultAsync(post1 =>
                        post1.Title == request.Title); //se nao existe vai retornar null

                if (postFound != null)
                {
                    throw new RestException(HttpStatusCode.Found, "post already exists");
                }

                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                var post = new Post
                {
                    Creationdate = DateTime.UtcNow,
                    Image = request.Image,
                    Title = request.Title,
                    Content = request.Content,
                    User = user
                };

                // var addPost = await _postRepository.Add(post);
                
              _postRepository.Add(post);

               var result = await _postRepository.Complete() < 0;
               //faz commit, para salvar as alteracoes
               
                if (result)
                {
                    throw new RestException(HttpStatusCode.InternalServerError,"AN ERROR OCCURRED");
                }

                return _mapper.Map<PostDto>(post);
                //retorna esse post para o mediator na classe PostsController

            }
               //retorna esse post para o mediator na classe PostsController
            }
        }
    }