# JsonLocalization

## Introduction

Since I don't like use resource file to localize asp.net core mvc application 
<br/>
so I tried to find the other formats for more easy way to use.
<br/>
And I found json format solution from stackoverflow:
<br/>
https://stackoverflow.com/questions/43615912/asp-net-core-localization-with-json-files
<br/>
And this repository is just written to middleware for easy to used.

## How to use

Add middleware in your Startup:

```c#
 public void ConfigureServices(IServiceCollection services)
        {
            services.AddJsonLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-GB"),
                        new CultureInfo("zh-TW")
                    };

                opts.DefaultRequestCulture = new RequestCulture("zh-TW");
                opts.SupportedCultures = supportCultures;
                opts.SupportedUICultures = supportCultures;
            });

            services.AddMvc()
                .AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix,
                opts =>
                {
                    opts.ResourcesPath = "Resources";
                })
                .AddDataAnnotationsLocalization();
        }

```

and 
```c#
  public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var options = app.ApplicationServices
                .GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(options.Value);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
```

and add localization.json file in your project:

```json 
[
  {
    "Key": "Hello",
    "Value": {
      "en-GB": "Hello",
      "zh-TW": "你好"
    }
  },
  {
    "Key": "Language",
    "Value": {
      "en-GB": "Language",
      "zh-TW": "語言"
    }
  }
]
```

Done, now you can use localizer in your controller or view
