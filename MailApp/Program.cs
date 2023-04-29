using MailApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMailService,SMTPMailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("SMTPMailSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/sendCustomMail", async (IMailService mailService,CustomMailModel mailModel) =>
{
    await mailService.SendCustomMessageAsync(mailModel);
    return "true";
});

app.MapPost("/sendDiscountMail", async (IMailService mailService,IWebHostEnvironment web, DiscountMailModel discountMailModel) =>
{
    MailModel mailModel = new MailModel()
    {
        Tos = discountMailModel.Tos,
        Subject = "Discount Alert",
        IsBodyHtml = true,
        Body = File.ReadAllText(web.WebRootPath + "/index.html")
    };

    await mailService.SendDiscountMessageAsync(mailModel);
    return "true";
});

app.Run();

