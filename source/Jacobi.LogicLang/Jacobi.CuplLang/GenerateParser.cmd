java -jar ../antlr4/antlr-4.12.0-complete.jar -Dlanguage=CSharp -message-format antlr -o Parser -package Jacobi.CuplLang.Parser -no-listener -visitor CuplLexer.g4
java -jar ../antlr4/antlr-4.12.0-complete.jar -Dlanguage=CSharp -message-format antlr -o Parser -package Jacobi.CuplLang.Parser -no-listener -visitor CuplParser.g4
