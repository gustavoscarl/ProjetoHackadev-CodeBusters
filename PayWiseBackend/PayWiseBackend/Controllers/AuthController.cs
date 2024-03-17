using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayWiseBackend.Domain.Context;

namespace PayWiseBackend.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : Controller
{
    private readonly PaywiseDbContext _context;
    private readonly IMapper _mapper;

    public AuthController(PaywiseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /*public async Task<IActionResult> Autenticar()
    {
    }*/
}
