using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

//Startup middleware düzenlemelerime başlıyorum
builder.Services.AddControllersWithViews();
//İlgili controller endpointlerini yakalamak için HttpClient ekliyorum (API)
builder.Services.AddHttpClient();

//Token ayarlamaları gerçekleştirilir
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    //hangi path ile login olur, sisteme tanıtıyorum
    opt.LoginPath = "/Account/SignIn";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";//eğer kullanıcının yetkisi yoksa
    opt.Cookie.SameSite = SameSiteMode.Strict; //sadece o domainde kullanılsın diye
    opt.Cookie.HttpOnly = true; //diyere js saldırılarından koruyorum
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //ssl ayarlaması cookie için
    opt.Cookie.Name = "JwtCookie";
}); //cookie ayarlamaları tamam artık sıra okumada 

var app = builder.Build();

app.UseStaticFiles();

//Routing ve endpoint middlewarelarımı çağırıyorum
app.UseRouting();

//middleware çağırılma sıralamasına dikkat ediyorum!
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
