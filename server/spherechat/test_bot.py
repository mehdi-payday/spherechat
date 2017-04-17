from chatterbot import ChatBot

chatbot = ChatBot(
	"Lucy", 
	trainer='chatterbot.trainers.ChatterBotCorpusTrainer',
	input_adapter='chatterbot.input.TerminalAdapter')

# chatbot.train("chatterbot.corpus.english")
# chatbot.train('chatterbot.corpus.english')
print "Type something man.."

while True:
    try:
        print chatbot.get_response(None)
    except(KeyboardInterrupt, EOFError, SystemExit):
       break

# chatbot.get_response("What is earth ?")
