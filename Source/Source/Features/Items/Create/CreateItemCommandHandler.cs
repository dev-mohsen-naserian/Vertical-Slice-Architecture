
namespace Source.Features.Items.Create;
public record CreateItemCommand(string Name,string Description,int Price,bool IsDeleted,DateTime CreatedAt):ICommand<CreateItemResult>;
public record CreateItemResult(int Id):IRequest<CreateItemCommand>;
internal class CreateItemCommandHandler : ICommandHandler<CreateItemCommand, CreateItemResult>
{
    public Task<CreateItemResult> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        //Business Logic to create a product
       
        throw new NotImplementedException();
    }
}
