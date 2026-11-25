builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress =
            new Uri("https://localhost:5001")
    });