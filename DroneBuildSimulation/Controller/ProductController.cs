using DroneBuildSimulation.DTOs.Requests;
using DroneBuildSimulation.Entities;
using DroneBuildSimulation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DroneBuildSimulation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _service;
    private readonly IWebHostEnvironment _webHostEnvironment; // Inject Environment untuk akses folder wwwroot

    public ProductController(IProductService service, IWebHostEnvironment webHostEnvironment)
    {
        _service = service;
        _webHostEnvironment = webHostEnvironment;
    }

    // ==========================================
    // 1. INDEX (LIST DATA)
    // ==========================================
    public async Task<IActionResult> Index()
    {
        var result = await _service.GetAllAsync();
        return View(result.Data);
    }

    // ==========================================
    // 2. CREATE (TAMBAH DATA)
    // ==========================================
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        // 📸 LOGIC UPLOAD GAMBAR BARU
        if (request.ImageFile != null)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            
            // Bikin folder kalau belum ada
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            // Generate nama file unik
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.ImageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save ke server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(fileStream);
            }

            // Set path untuk disimpan di DB
            request.ImageUrl = "/images/" + uniqueFileName;
        }

        var result = await _service.CreateAsync(request);

        if (result.Success)
        {
            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", result.Message);
        return View(request);
    }

    // ==========================================
    // 3. EDIT (UPDATE DATA)
    // ==========================================
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        // Ambil data lama
        var result = await _service.GetByIdAsync(id);
        
        if (!result.Success || result.Data == null) 
            return NotFound();

        // Mapping dari Response (DB) ke Request (Form Edit)
        // Kita lakukan manual di sini agar Form terisi data lama
        var updateDto = new UpdateProductRequest
        {
            Id = result.Data.Id,
            Name = result.Data.Name,
            Brand = result.Data.Brand,
            Type = Enum.Parse<ComponentType>(result.Data.Type), // Convert String ke Enum
            Price = result.Data.Price,
            Weight = result.Data.Weight,
            
            // Specs
            Rating = result.Data.Rating,
            Capacity = result.Data.Capacity,
            Range = result.Data.Range,
            Size = result.Data.Size,
            
            // Gambar Lama (Penting untuk ditampilkan di preview)
            ImageUrl = result.Data.ImageUrl 
        };

        return View(updateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateProductRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        // 📸 LOGIC GAMBAR SAAT EDIT
        // Kita butuh data lama untuk tahu path gambar lama
        var oldDataResult = await _service.GetByIdAsync(request.Id);
        string? oldImageUrl = oldDataResult.Data?.ImageUrl;

        // Jika User Upload Gambar Baru
        if (request.ImageFile != null)
        {
            // 1. Hapus gambar lama fisik dari folder (biar gak nyampah)
            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, oldImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            // 2. Upload gambar baru
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.ImageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(fileStream);
            }

            request.ImageUrl = "/images/" + uniqueFileName;
        }
        else
        {
            // Jika tidak upload baru, pertahankan URL lama
            request.ImageUrl = oldImageUrl;
        }

        var result = await _service.UpdateAsync(request);

        if (result.Success)
        {
            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", result.Message);
        return View(request);
    }

    // ==========================================
    // 4. DELETE (HAPUS DATA)
    // ==========================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        // 1. Cek data dulu untuk ambil path gambarnya
        var result = await _service.GetByIdAsync(id);
        
        if (result.Success && result.Data != null)
        {
            // 2. Hapus File Gambar Fisik di wwwroot
            if (!string.IsNullOrEmpty(result.Data.ImageUrl))
            {
                // Hapus tanda '/' di depan path (/images/..) agar Path.Combine valid
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, result.Data.ImageUrl.TrimStart('/'));
                
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // 3. Hapus Data di Database
            await _service.DeleteAsync(id);
            TempData["Success"] = "Produk berhasil dihapus";
        }
        else
        {
            TempData["Error"] = "Gagal menghapus produk";
        }

        return RedirectToAction(nameof(Index));
    }
}