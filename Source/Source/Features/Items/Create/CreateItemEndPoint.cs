
namespace Source.Features.Items.Create;
public record CreateItemRequest(string Name, string Description, int Price, bool IsDeleted, DateTime CreatedAt);
public record CreateItemResponse(int Id);
public class CreateItemEndPoint : ICarterModule
{
    void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Items", async (CreateItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateItemCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateItemResponse>();
            return Results.Created($"/items/{response.Id}", response);
        })
            .WithName("CreateItem")
            .Produces<CreateItemCommand>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Item")
            .WithDescription("Create Item");
    }
}
