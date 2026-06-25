using Microsoft.AspNetCore.Server.Kestrel.Core;
using Toolkit_API.Application.Analysis;
using Toolkit_API.Application.App_Services.User;
using Toolkit_API.Application.Application_Services.EmailServices;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Application_Services.Operations;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;
using Toolkit_API.Infrastructure.Repositories;
using Toolkit_API.Infrastructure.Security;
using Toolkit_API.Infrastructure.Security.Jwt;
using Toolkit_API.Infrastructure.Services;
using Toolkit_API.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var connetionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? throw new InvalidOperationException("'DB_CONNECTION' not found");
var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET")
    ?? throw new InvalidOperationException("'JWT_SECRET Not found");

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<Login>();
builder.Services.AddTransient<CreateUser>();
builder.Services.AddTransient<FileHasher>();
builder.Services.AddHttpClient<ICallExternalAPI, ExternalCalls>();
builder.Services.AddTransient<HandleResult>();
builder.Services.AddTransient<IFileAnalysis, FileAnalysis>();
builder.Services.AddTransient<ExtractedStrings>();
builder.Services.AddTransient<IEmailServices, EmailServices>();
builder.Services.AddTransient<ZipPolicies>();
builder.Services.AddTransient<IZipHandler, HandleZip>();
builder.Services.AddTransient<HandleFolder>();
builder.Services.AddTransient<HandleZip>();
builder.Services.AddTransient<HandleResult>();
builder.Services.AddTransient<StaticFileAnalysis>();
builder.Services.AddTransient<FileAnalysisResult>();
builder.Services.AddTransient<FolderInfo>();

builder.Services.AddTransient<ScoringAlg>(
    options => new ScoringAlg(options.GetRequiredService<IFileAnalysis>(),
    options.GetRequiredService<HandleResult>(),
    0.0,
    options.GetRequiredService<ExtractedStrings>()

));

builder.Services.AddTransient<IUserRepo, SqlUserRepo>(options =>
    new SqlUserRepo(options.GetRequiredService<IPasswordHasher>(), connetionString)
);

builder.Services.AddTransient<IAdminRepo, AdminRepository>(options =>
    new AdminRepository(connetionString)
);

builder.Services.AddTransient<HandleFolder>(options =>
    new HandleFolder(options.GetRequiredService<FileScanOps>(), new FolderInfo())
);

builder.Services.AddTransient<HandleZIP>(options =>
    new HandleZIP(
    options.GetRequiredService<HandleZip>(),
    options.GetRequiredService<ZipPolicies>())
);

builder.Services.AddTransient<IGenerateToken, TokenGenerator>(options =>
    new TokenGenerator(jwtKey)
);

builder.Services.AddTransient<NewLetter>(options =>
    new NewLetter(options.GetRequiredService<IEmailServices>())
);

builder.Services.AddTransient<StaticFileAnalysis>(options =>
    new StaticFileAnalysis(options.GetRequiredService<IFileAnalysis>(),
        options.GetRequiredService<ScoringAlg>(),
        options.GetRequiredService<ExtractedStrings>()

    )
);

builder.Services.AddTransient<IFileScanRepo, FileScanRepo>(options =>
    new FileScanRepo(options.GetRequiredService<FileHasher>(),
    connetionString
    )
);

builder.Services.AddTransient<FileScanOps>(options =>
    new FileScanOps(options.GetRequiredService<IFileScanRepo>(),
    options.GetRequiredService<ICallExternalAPI>(),
    options.GetRequiredService<HandleResult>(),
    options.GetRequiredService<StaticFileAnalysis>(),
    options.GetRequiredService<FileHasher>(),
    options.GetRequiredService<HandleZIP>()
    )

);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(b =>
{
    b.AddConsole();
    b.SetMinimumLevel(LogLevel.Debug);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.Configure<KestrelServerOptions>(options =>
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10)
);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
