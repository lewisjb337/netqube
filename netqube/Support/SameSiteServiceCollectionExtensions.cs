﻿namespace netqube.Support
{
    public static class SameSiteServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSameSiteNoneCookies(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
            });

            return services;
        }

        private static void CheckSameSite(CookieOptions options)
        {
            if (options is { SameSite: SameSiteMode.None, Secure: false })
            {
                options.SameSite = SameSiteMode.Unspecified;
            }
        }
    }
}
