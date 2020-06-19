&#9745; Crair um boneco com as animações:
- &#9745; Idle
- &#9745; Attack
- &#9745; posicionar o bonecos um em cada lado do tabuleiro
- &#9745; isPlayerTeam or not


Criar Battlehandler para gerenciar a lógica do sistema de batalha
- Implementar os estados de:
 - WaitingForPlayer
 - Busy
Dessa forma esperamos o ataque terminar para poder atacar novamente


CaracterBattle classe responsável por gerenciar as animações dos bonecos
- Criar um método Setup() para ser utilizado como um construtor
- Método attack para ativar a animação
  - Parametros, quem é o CharacterBattle que ataca e um callback para quando o ataque terminar
  - Ataque vai até o outro personagem e então volta para a posição inicial
   - Implementar no Update() a troca dos estados
   - O estado sliding Vector3.Distance(GetPosition(), slideTargetPosition) < reachedDistance
    - tranform.position += (slideTargetPosition - GetPosition()) * slideSpeed * Time.deltaTime
    - Criar a função SlideToPosition
    - slideTargetPosition é a posição do enemy

Space será o botão para acionar o ataque
