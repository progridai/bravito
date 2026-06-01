using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Chat.Models;

namespace Bravito.Application.Chat.Interfaces
{
    public interface IAssistenteChatService
    {
        Task<EnviarMensagemChatResponse> EnviarMensagemAsync(EnviarMensagemChatRequest request, UsuarioAutenticado usuario, CancellationToken cancellationToken);
    }
}
