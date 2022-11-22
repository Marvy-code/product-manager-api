using ProductManager.Composition;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
CompositionRoot.Compose(services);

services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssemblyContaining<CreateProductCategoryInputValidator>();

services.AddMediatR(typeof(CreateProductCategoryCommandHandler));

services.AddControllers();
services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder
        => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
