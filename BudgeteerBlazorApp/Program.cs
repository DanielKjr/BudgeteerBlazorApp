using Budgeteer.Plugins.InMemory;
using Budgeteer.UseCases;
using Budgeteer.UseCases.Expenses;
using Budgeteer.UseCases.PluginInterfaces;
using BudgeteerBlazorApp.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthenticationCore();

//user authentication
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserSession>();

//Data for current user
builder.Services.AddScoped<IExpenseRepository, ExpensesRepository>();

//Register mapping between abstraction and implementation
//builder.Services.AddSingleton<IExpenseRepository, ExpensesRepository>();

//Singleton
//When application requires an instance of the implementation, it creates one and provides the instance
//Also stores the instance in the application, which stays for as long as the app is running


//Transient
//Whenever we require an instance of a class, it will create one
//Framework does not store the instance anywhere
builder.Services.AddTransient<IViewExpensesByName, ViewExpensesByName>();
builder.Services.AddTransient<IAddExpenseUseCase, AddExpenseUseCase>();
builder.Services.AddTransient<IEditExpenseUseCase, EditExpenseUseCase>();
builder.Services.AddTransient<IViewExpenseByIdUseCase, ViewExpenseByIdUseCase>();
builder.Services.AddTransient<IDeleteExpenseByIdUseCase, DeleteExpenseByIdUseCase>();
builder.Services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddTransient<IViewExpensesByUserReference, ViewExpensesByUserReference>();


//AddScoped -> another lifetime management 
//builder.Services.AddScoped<>();
//The instance will be created and stored by application, but only for as long as the SignalR channel lives
//Meaning if you refresh browser you disconnect from SingalR and instance will be disposed
//As long as the connection isn't broken, the instance will be re-used for as long as the application is running

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
