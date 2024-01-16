
 - [x] Compilando
 - [x] Projeto organizado
 - [x] Fácil entendimento
 - [x] Código Limpo
 - [x] Seguindo padrão de escrita em CamelCase
 - [x] Baseado em Clean Architeture
 - [x] .Net 8
 - [x] C# 12
 - [x] Documentação
	 * <font color="orange">Há um erro na configuração da documentação pois,  sempre ao pedir uma requisição está pedindo a versão da API. </font>

# Autenticação
* <font color="orange"> Prestar mais atenção nas validações e informações ao usuário. Só descobri que o UserName deve ser um e-mail, porque olhei a validação, pois na doc não tem nada especificando. Havia colocado um nome e não passava. Mas foi colocado atributo de validação como e-mail. 
* Mensagens de retorno em qualquer situação para usuário, deve ser no idioma dele, no nosso caso em pt-BR.  Um pedido foi que usar nomes para a <font color="red"><strong>programação</strong></font> em inglês.
* Fiquei meio perdido em fazer o cadastro de um usuário para teste. Pois na lógica posso cadastrar, pois o endpoint de Register-user está anonymous, mas ao mesmo tempo, tem uma lógica que a inclusão somente pode ser feita por Administrator, mas como forço a minha requisição em ser um Administrator? O ideal aqui é que o usuário base, fosse inserido na migrations, pelo menos para fins de desenvolvimento.  Como passei ao Caio o endpoint de Register-User, deve ser acessado somente depois de autenticado.  Esta ponderação é somente para que possa lembrar disto em um futuro ok. Segurança sempre em 1o. lugar, depois da funcionalidade. 
*  Na classe UserService no método RegisterUserAsync, temos a notificação que deveria estar em pt.Br, fiquemos atendo a isto.  Mas seu funcionamento está ok. Só caio na lógica de não ser admin.      
* <font color="red">Prestar atenção  pois quase fica impossível de fazer o registro do usuário, pois não há validação da senha que é fraca e o identity não aceita por padrão, ou seja, o serviço deve fazer uso do objeto quando ele estiver válido. Sei que no caso do Identity ele tem as validações e posso tratá-lo como serviço, mas o ideal que não dependamos de terceiros.  E o retorno do Identity virá todo no idioma padrão deles, melhor que fosse em pt-Br.

```json
          {
            "sucess": false,
            "errors": [
            "O CPF é inválido!",
            "Passwords must have at least one non alphanumeric character.",
            "Passwords must have at least one lowercase ('a'-'z').",
            "Passwords must have at least one uppercase ('A'-'Z')."
          ]
          }
```
* Cabe uma explicação que os métodos utilizados em `RegisterUserAsync`,  as validações, principalmente as de autenticação, não são necessárias, tantas validações, me dá impressão que foram sendo colocadas para funcionar e não analisadas. Pois só o atributo [Authorize] na controler já iria eliminar a necessidade de verificar se está autenticado. Outro ponto que autenticado ele já viria com a roles no token ok.  


* ### Tudo isto que foi passado acima é só para explicação, nem tudo é erro, alguns pontos podem ter um custo mais alto, mas estão funcionais.
</font>

# Usuários
*<font color="orange"> Get-All-Users-roles, deveria ter acesso só com autenticação. 

</font>

### <font color="red">Não verifiquei tudo!
# Produtos
### <font color="red">Não verifiquei tudo!
# Unidades
### <font color="red">Não verifiquei tudo!
# Pessoas
### <font color="red">Não verifiquei tudo!
# Validações
* <font color="orange">Validações no padrão .net são funcionais e eficazes, mas  como temos recursos melhores como o FluentValidator, onde temos mais controles das mesmas, é melhor utilizá-lo. Não falei nada no desafio, mas seria um plus. O que fez está correto, mas outros que usaram sempre o FluentValidator, ganhariam mais pontos ok. #ficadica
* ### <font color="red">Em todos métodos nos serviços que usam a validação, o ideal é mapear o request e não o objeto mapeado, isto porque, pode haver algum erro no request e não conseguir fazer o mapeamento, dando uma exception não tratada. Sempre validamos e estando válido, passamos adiante ok.  Atentar a isto.

</font>

# Utilitários

# Contextos
* <font color="red"><strong>Verificar para que Migrations rode somente em um ambiente que não em produção. Muito cuidado com isto. </strong>

</font>

# Logs
### Não encontrei Arquivo de logs.

# Geral
* <font color="oranfe"> Sei que em muitos cursos os retornos da API são personalizados, mas caso não tenha sido pedido em um desafio ou mesmo  na documentação, o retorno deve ser sempre o objeto diretamente ou simplesmente o `IActionResul<object>`, assim temos um retorno padrão do http. Não está errado o que fizeres ok. Só um dica. Apesar que neste desafio, deixei aberto para todos, o CustomResponse é uma boa sacada também, pois se for uma API específica, podemos ter retornos bem específicos. 
* 
### <font color="red"> Métodos get de qualquer outro endpoint, deve necessitar de autenticação, pois, caso não tenha, qualquer usuário poderá acessar todos os dados. Segurança ok.</font>
</font>

# Avaliação.
 ### <font color="blue"> No geral, ficou bem estrututurado. A ideia do desafio era usar os conhecimentos que aprendemos ao longo do mês de dezembro, para que possamos utilizar e entender no geral o funcionamento. Claro que há inúmeros pontos que podemos melhorar na escrita do código e claro baseado na minha experiência e não que eu esteja certo, mas é mais funcional e o algoritimo mais barato. Não consegui analisar todos os pontos, mas queria ver o que aprederam/entenderam e a capacidade de trabalharem em equipe para solucionar alguns problemas que hoje, no .Net Framework já fariamos perfeitamente. No mais, estamos no caminho certo, quando iniciar o mobile, já saberá como a resposta vem e o quão poderá ser difícil a API entregar os dados como o front quer. rsrsr
 
 # <font color="blue">Parabéns!
> Written with [StackEdit](https://stackedit.io/).
