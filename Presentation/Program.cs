using Business.Abstract;
using Business.Concrete;
using Data.Abstract;
using Data.Concrete.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.EmailServices.Abstract;
using Presentation.Identity;
using Presentation.Identity.Abstract;
using Presentation.Session;
using Shared.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<ShopContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString"),
    e=> e.MigrationsAssembly("Presentation"));
});

builder.Services.AddDbContext<IdentityContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString"));
});

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    // password
    options.Password.RequireDigit = true; 
    options.Password.RequireLowercase = true; 
    options.Password.RequireUppercase = true; 
    options.Password.RequiredLength = 6; 
    options.Password.RequireNonAlphanumeric = true; 

    // lockout
    options.Lockout.MaxFailedAccessAttempts = 5; 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); 
    options.Lockout.AllowedForNewUsers = true; 

    options.User.RequireUniqueEmail = true; 
    options.SignIn.RequireConfirmedEmail = true; 
    options.SignIn.RequireConfirmedPhoneNumber = false; 
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/hesap/girisyap"; 
    options.LogoutPath = "/hesap/cikisyap"; 
    options.AccessDeniedPath = "/erisimengellendi"; 
    options.SlidingExpiration = true; 
    options.ExpireTimeSpan = TimeSpan.FromDays(30);

    options.Cookie = new CookieBuilder
    {
        HttpOnly = true, 
        Name = ".OnlineShopApp.Security.Cookie",
        SameSite = SameSiteMode.Strict,       
    };
});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICartService,CartManager>();
builder.Services.AddScoped<IOrderService,OrderManager>();
builder.Services.AddScoped<IOrderStateService,OrderStateManager>();

builder.Services.AddScoped<ISignService,SignService>();
builder.Services.AddScoped<IUserService,UserService>();

builder.Services.AddScoped<SessionManager>();

builder.Services.AddScoped<IEmailSender,SmtpEmailSender>( i => 
    new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"]!,
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:UserName"]!,
        builder.Configuration["EmailSender:Password"]!
    )
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".OnlineShopApp.Cart.Cookie";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseSession();

// Cart
app.MapControllerRoute
(
    name: "cartindex",
    pattern: "sepetim",
    defaults: new { controller="Cart", action="Index" }
);

// Order
app.MapControllerRoute
(
    name: "checkout",
    pattern: "odeme",
    defaults: new { controller="Order", action="Checkout" }
);

app.MapControllerRoute
(
    name: "orderindex",
    pattern: "siparislerim",
    defaults: new { controller="Order", action="Index" }
);

// Admin
app.MapControllerRoute
(
    name: "adminindex",
    pattern: "admin/anasayfa",
    defaults: new { controller="Admin", action="Index" }
);

app.MapControllerRoute
(
    name: "adminorderlist",
    pattern: "admin/siparislistesi/{siparisdurumu?}",
    defaults: new { controller="Admin", action="OrderList" }
);

app.MapControllerRoute
(
    name: "adminproductlist",
    pattern: "admin/urunlistesi",
    defaults: new { controller="Admin", action="ProductList" }
);

app.MapControllerRoute
(
    name: "adminaddproduct",
    pattern: "admin/urunekle",
    defaults: new { controller="Admin", action="AddProduct" }
);

app.MapControllerRoute
(
    name: "admindeleteproduct",
    pattern: "admin/urunsil/{id?}",
    defaults: new { controller="Admin", action="DeleteProduct" }
);

app.MapControllerRoute
(
    name: "adminupdateproduct",
    pattern: "admin/urunguncelle/{id?}",
    defaults: new { controller="Admin", action="UpdateProduct" }
);

app.MapControllerRoute
(
    name: "adminaddcategory",
    pattern: "admin/kategoriekle",
    defaults: new { controller="Admin", action="AddCategory" }
);

app.MapControllerRoute
(
    name: "admincategorylist",
    pattern: "admin/kategorilistesi",
    defaults: new { controller="Admin", action="CategoryList" }
);

app.MapControllerRoute
(
    name: "adminupdatecategory",
    pattern: "admin/kategoriguncelle/{id?}",
    defaults: new { controller="Admin", action="UpdateCategory" }
);

app.MapControllerRoute
(
    name: "admindeletecategory",
    pattern: "admin/kategorisil/{id?}",
    defaults: new { controller="Admin", action="DeleteCategory" }
);


// Auth
app.MapControllerRoute
(
    name: "register",
    pattern: "hesap/uyeol",
    defaults: new { controller="Auth", action="Register" }
);

app.MapControllerRoute
(
    name: "login",
    pattern: "hesap/girisyap",
    defaults: new { controller="Auth", action="Login" }
);

app.MapControllerRoute
(
    name: "logout",
    pattern: "hesap/cikisyap",
    defaults: new { controller="Auth", action="Logout" }
);

app.MapControllerRoute
(
    name: "emailconfirm",
    pattern: "hesap/uyelikonayi",
    defaults: new { controller="Auth", action="ConfirmEmail" }
);

app.MapControllerRoute
(
    name: "fargotpassword",
    pattern: "hesap/sifremiunuttum",
    defaults: new { controller="Auth", action="FargotPassword" }
);

app.MapControllerRoute
(
    name: "resetpassword",
    pattern: "hesap/sifreyenile",
    defaults: new { controller="Auth", action="ResetPassword" }
);

app.MapControllerRoute
(
    name: "accessdenied",
    pattern: "erisimengellendi",
    defaults: new { controller="Auth", action="AccessDenied" }
);

// Product
app.MapControllerRoute
(
    name: "search",
    pattern: "arama",
    defaults: new { controller="Product", action="Search" }
);

app.MapControllerRoute
(
    name: "products",
    pattern: "urunler/{kategori?}",
    defaults: new { controller="Product", action="Products" }
);

app.MapControllerRoute
(
    name: "productdetails",
    pattern: "urun/{url}",
    defaults: new { controller="Product", action="Details" }
);

app.MapControllerRoute
(
    name: "about",
    pattern: "hakkimizda",
    defaults: new { controller="Home", action="About" }
);

app.MapControllerRoute
(
    name: "contact",
    pattern: "iletisim",
    defaults: new { controller="Home", action="Contact" }
);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


IdentitySeed.Seed(app,builder.Configuration).Wait();

app.Run();
