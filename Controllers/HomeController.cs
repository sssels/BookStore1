using BookStore1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register()
    {
        // Register sayfasına yönlendirme
        return RedirectToAction("Register", "Account");
    }

    public IActionResult Login()
    {
        // Login sayfasına yönlendirme
        return RedirectToAction("Login", "Account");
    }

    public IActionResult ViewBooks()
    {
        // ViewBooks sayfasına yönlendirme
        return RedirectToAction("Index", "Books");
    }

    public IActionResult Cart()
    {
        // Cart sayfasına yönlendirme
        return RedirectToAction("Cart", "Cart");
    }
}