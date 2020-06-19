&#9745; Criação de um prefab para ser instancia

&#9745; Criação de um script de teste
 - &#9745; Implementar no clique a instanciação do popup

&#9745; Classe de DamagePopup, responsável por saber instanciar um objeto e para definir suas propriedades
 - &#9745; static método instanciador
 - &#9745; método de setup
 - &#9745; método de atualização do popup para ter o efetio de fade (alterando o alpha a cor, Color)
 - &#9745; quando alpha == 0 Destroy(gameobject)
 - corrigir o problema com o sortingOrder, para garantir que os danos aparecem em cima de danos antigos

&#9745; Classe de GameAssets, responsável por definir os assets do jogo que são instanciados dinamicamente

Criar uma camada quer será acima de todas as outras