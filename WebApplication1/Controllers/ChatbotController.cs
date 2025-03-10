using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using Newtonsoft.Json;
using WebApplication1.Models;

public class ChatbotController : Controller
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public ChatbotController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClient = httpClientFactory.CreateClient("ChatbotClient");
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] string prompt)
    {
        try
        {
            var _apiUrl = _config["CHATBOT_API"];
            var response = await _httpClient.GetAsync($"{_apiUrl}chatbot/ask?prompt={Uri.EscapeDataString(prompt)}&knowledge=true");
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonDocument.Parse(jsonResponse)
                .RootElement
                .GetProperty("message")
                .GetString();
            
            string? message = MarkdownParser.ParseMarkdown(result);
            ViewData["ChatHistory"] = message;
            ViewBag.ChatHistory = ViewData["ChatHistory"];
            return Json(new { success = true, message });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error: " + ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetChatSessions()
    {
        try
        {
            var _apiUrl = _config["CHATBOT_API"];
            var response = await _httpClient.GetAsync($"{_apiUrl}chatbothistory/sessions");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to fetch chat session.");
            }
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var chatSessions = JsonConvert.DeserializeObject<List<ChatSessionModel>>(jsonResponse);
            
            return Json(new { success = true, chatSessions });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error: " + ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetChatHistory(string id)
    {
        try
        {
            var _apiUrl = _config["CHATBOT_API"];
            var response = await _httpClient.GetAsync($"{_apiUrl}chatbothistory/history/{Uri.EscapeDataString(id)}");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to fetch chat history.");
            }
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var chatHistory = JsonConvert.DeserializeObject<List<ChatHistoryModel>>(jsonResponse);
            
            return Json(new { success = true, chatHistory });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error: " + ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var _apiUrl = _config["CHATBOT_API"];
            var uploadUrl = $"{_apiUrl}weaviate/uploadData";

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                var response = await _httpClient.PostAsync(uploadUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Failed to upload file.");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return Json(new { success = true, message = "File uploaded successfully.", data = jsonResponse });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error: " + ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUploadedFiles()
    {
        try
        {
            var _apiUrl = _config["CHATBOT_API"];
            var response = await _httpClient.GetAsync($"{_apiUrl}Knowledge/GetListFiles");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to fetch chat history.");
            }
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var listKnowledge = JsonConvert.DeserializeObject<List<UploadedFileModel>>(jsonResponse);
            
            return Json(new { success = true, listKnowledge });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error: " + ex.Message });
        }
    }
}