using InventoryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Dapper;
using System.Diagnostics;
using NuGet.Packaging.Signing;


namespace InventoryManagement.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=cdac;User=root;Password=2019;";
         
// GET: InventoriesController
public ActionResult Index()
        {

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var inventory = connection.Query<Inventory>("SELECT * FROM InventoryTable");
                return View(inventory);
            }
        }

        // GET: InventoriesController/Details/5
        public ActionResult Details(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM InventoryTable WHERE Id = @Id";
                var inventory = connection.QuerySingleOrDefault<Inventory>(query, new { Id = id });

                if (inventory == null)
                {
                    return NotFound();
                }

                return View(inventory);
            }
        }

        // GET: InventoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory inventory)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO InventoryTable (Inventory_ID,Inventory_Name,Inventory_Price,Inventory_Quantity) VALUES (@Inventory_ID,@Inventory_Name,@Inventory_Price,@Inventory_Quantity)";
                    Trace.WriteLine(query);
                    connection.Execute(query,inventory);
                    return RedirectToAction(nameof(Index));
                }

                
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var inventory = connection.QuerySingleOrDefault<Inventory>("SELECT * FROM InventoryTable WHERE Id = @Id", new { Id = id });
                if (inventory == null)
                {
                    return NotFound();
                }
                return View(inventory);
            }

            
        }

        // POST: InventoriesController/Edit/{inventory}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inventory inventory)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "UPDATE InventoryTable SET Inventory_Name = @Inventory_Name, Inventory_Price = @Inventory_Price, Inventory_Quantity=@Inventory_Quantity, Inventory_ID = @Inventory_ID WHERE Id = @Id";
                    Trace.WriteLine(query);
                    Trace.WriteLine(connection.Execute(query, inventory));                   
                }
                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM InventoryTable WHERE Id = @Id";
                var inventory = connection.QuerySingleOrDefault<Inventory>(query, new { Id = id });

                if (inventory == null)
                {
                    return NotFound();
                }

                return View(inventory);
            }
            
        }

        // POST: InventoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Inventory inventory)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM InventoryTable WHERE Id = @Id";
                    Trace.WriteLine(query);
                    connection.Execute(query, inventory);
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch
            {
                return View();
            }
        }
    }
}
