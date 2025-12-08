using AutoMapper;
using DroneBuildSimulation.Constants; // Pastikan namespace ini ada
using DroneBuildSimulation.DTOs.Requests;
using DroneBuildSimulation.Services.Interfaces; // FileService & ProductService
using Microsoft.AspNetCore.Mvc;

namespace DroneBuildSimulation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IFileService _fileService; // Service baru untuk handle file
        private readonly IMapper _mapper;           // AutoMapper

        // Constructor Injection
        public ProductController(IProductService service, IFileService fileService, IMapper mapper)
        {
            _service = service;
            _fileService = fileService;
            _mapper = mapper;
        }

        // ==========================================
        // 1. INDEX (LIST DATA)
        // ==========================================
        public async Task<IActionResult> Index()
        {
            // Service me-return ServiceResult<List<ProductResponse>>
            var result = await _service.GetAllAsync();
            
            // Kita langsung kirim DTO Response ke View
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

            try
            {
                // 📸 LOGIC UPLOAD: Delegasikan ke FileService
                if (request.ImageFile != null)
                {
                    // Upload file dan dapatkan nama uniknya
                    string fileName = await _fileService.UploadFileAsync(request.ImageFile, FileConstants.ProductImagesFolder);
                    
                    // Simpan path relatif ke database (misal: "images/products/unik.jpg")
                    // Atau simpan nama filenya saja, tergantung preferensi Anda. 
                    // Di sini kita asumsikan simpan path relatif agar mudah diakses di View.
                    request.ImageUrl = Path.Combine(FileConstants.ProductImagesFolder, fileName).Replace("\\", "/");
                }

                // Panggil Service untuk simpan ke DB
                var result = await _service.CreateAsync(request);

                if (result.Success)
                {
                    TempData["Success"] = "Product created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                // Jika Service gagal (misal validasi bisnis)
                ModelState.AddModelError("", result.Message);
            }
            catch (ArgumentException ex) // Tangkap error validasi ekstensi file
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }

            return View(request);
        }

        // ==========================================
        // 3. EDIT (UPDATE DATA)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Ambil data dari Service (Berupa ProductResponse)
            var result = await _service.GetByIdAsync(id);

            if (!result.Success || result.Data == null)
            {
                return NotFound();
            }

            // 🔄 MAPPING OTOMATIS: ProductResponse -> UpdateProductRequest
            // Tidak perlu manual mapping satu per satu lagi!
            var updateDto = _mapper.Map<UpdateProductRequest>(result.Data);

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            try
            {
                // 📸 LOGIC UPLOAD SAAT EDIT
                if (request.ImageFile != null)
                {
                    // 1. Upload Gambar Baru
                    string newFileName = await _fileService.UploadFileAsync(request.ImageFile, FileConstants.ProductImagesFolder);
                    
                    // 2. Hapus Gambar Lama (Clean Up)
                    // Mengambil nama file lama dari ImageUrl yang ada di hidden input
                    if (!string.IsNullOrEmpty(request.ImageUrl))
                    {
                        // Kita ambil nama filenya saja dari path
                        string oldFileName = Path.GetFileName(request.ImageUrl);
                        _fileService.DeleteFile(oldFileName, FileConstants.ProductImagesFolder);
                    }

                    // 3. Update Property dengan Path Baru
                    request.ImageUrl = Path.Combine(FileConstants.ProductImagesFolder, newFileName).Replace("\\", "/");
                }

                // Panggil Service Update
                var result = await _service.UpdateAsync(request);

                if (result.Success)
                {
                    TempData["Success"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }

            return View(request);
        }

        // ==========================================
        // 4. DELETE (HAPUS DATA)
        // ==========================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 1. Ambil data dulu untuk mendapatkan nama file gambar (sebelum dihapus DB-nya)
            var getResult = await _service.GetByIdAsync(id);

            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }

            // 2. Hapus Data di Database
            var deleteResult = await _service.DeleteAsync(id);

            if (deleteResult.Success)
            {
                // 3. Hapus File Fisik (Hanya jika DB sukses dihapus)
                if (!string.IsNullOrEmpty(getResult.Data.ImageUrl))
                {
                    string fileName = Path.GetFileName(getResult.Data.ImageUrl);
                    _fileService.DeleteFile(fileName, FileConstants.ProductImagesFolder);
                }

                TempData["Success"] = "Product deleted successfully.";
            }
            else
            {
                // Handle jika DB gagal hapus (misal ada relasi foreign key)
                TempData["Error"] = deleteResult.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}