using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using Aplication.Interfaces;
using AutoMapper;
using Doiman;
using Persistence;

namespace Infrastruture.Services;

public class PostRepository: IPostRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PostRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public void Add(Post post)
    {
        
        //return await _context.Posts.AddAsync(post);
        var data = _context.Set<Post>().Add(post);
        
        //faz commit, para salvar as alteracoes
        // return data;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }
}