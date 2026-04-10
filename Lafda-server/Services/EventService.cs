using Lafda.Dtos;
using Lafda.Entities;
using Lafda.Enums;
using Lafda.Repositories.Interfaces;
using Lafda.Services.Interfaces;
using Pgvector;
using System.Net.Http.Json;

namespace Lafda.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMainEventRepository _mainEventRepository;
    private readonly HttpClient _httpClient;

    public EventService(IEventRepository eventRepository, IMainEventRepository mainEventRepository, HttpClient httpClient)
    {
        _eventRepository = eventRepository;
        _mainEventRepository = mainEventRepository;
        _httpClient = httpClient;
    }

    // ➕ CREATE
    public async Task<ApiResponse<EventResponseDto>> CreateEventAsync(CreateEventDto dto, int userId)
    {
        // first create the embedding
        MainEvent mainEvent = await _mainEventRepository.GetByIdAsync(dto.MainEventId);
        if (mainEvent == null)
            return ApiResponse<EventResponseDto>.Fail("MainEvent not found");

        EmbeddingPayloadDto embeddingData = new EmbeddingPayloadDto
        {
            Title = mainEvent.Title,
            Description = mainEvent.Description,
            Actor1 = dto.Actor1,
            Actor2 = dto.Actor2,
            EventType = dto.EventType,
            SubEventType = dto.SubEventType,
            EventDate = dto.EventDate
        };

        // call embedder (py api)
        var text =
            $"{mainEvent.Title}. {mainEvent.Description}. " +
            $"It involved {dto.Actor1}" +
            (string.IsNullOrEmpty(dto.Actor2) ? "" : $" and {dto.Actor2}") +
            $". The event type was {dto.EventType} with subtype {dto.SubEventType}. " +
            $"It occurred on {dto.EventDate}.";

        var embeddingArray = await GetEmbeddingAsync(text);

        // posting for db

        var entity = new Event
        {
            DisorderType = dto.DisorderType,
            EventType = dto.EventType,
            SubEventType = dto.SubEventType,
            Actor1 = dto.Actor1,
            Actor2 = dto.Actor2 ?? "",
            Notes = dto.Notes ?? "",
            HumanCasualties = dto.HumanCasualties,
            Longitude = dto.Longitude,
            Latitute = dto.Latitute,
            EventDate = dto.EventDate,
            UserId = userId,
            MainEventId = dto.MainEventId,
            EventStatus = PostStatusEnum.Waiting,
            Embedding = new Vector(embeddingArray)
        };

        await _eventRepository.AddAsync(entity);
        await _eventRepository.SaveChangesAsync();

        return ApiResponse<EventResponseDto>.Ok(Map(entity), "Event Creation Posted for Review");
    }

    public async Task<ApiResponse<EventResponseDto>> GetByIdAsync(int id)
    {
        var entity = await _eventRepository.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<EventResponseDto>.Fail("Event not found");

        return ApiResponse<EventResponseDto>.Ok(Map(entity));
    }

    // 📋 GET ALL
    public async Task<ApiResponse<List<EventResponseDto>>> GetAllAsync()
    {
        var events = await _eventRepository.GetAllAsync();

        return ApiResponse<List<EventResponseDto>>.Ok(
            events.Select(Map).ToList()
        );
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var entity = await _eventRepository.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<string>.Fail("Event not found");

        _eventRepository.Delete(entity);
        await _eventRepository.SaveChangesAsync();

        return ApiResponse<string>.Ok("Deleted successfully");
    }

    private static EventResponseDto Map(Event e)
    {
        return new EventResponseDto
        {
            Id = e.Id,
            DisorderType = e.DisorderType,
            EventType = e.EventType,
            SubEventType = e.SubEventType,
            Actor1 = e.Actor1,
            Actor2 = e.Actor2,
            Notes = e.Notes,
            HumanCasualties = e.HumanCasualties,
            Longitude = e.Longitude,
            Latitute = e.Latitute,
            EventDate = e.EventDate,
            UserId = e.UserId,
            MainEventId = e.MainEventId
        };
    }


    public async Task<float[]> GetEmbeddingAsync(string text)
    {
        var request = new EmbedRequestDto
        {
            Text = text
        };

        var response = await _httpClient.PostAsJsonAsync(
            "http://127.0.0.1:8000/embed",
            request
        );

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<EmbeddingResponseDto>();

        return result!.Embedding.ToArray();
    }

}