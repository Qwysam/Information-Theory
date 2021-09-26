# -*- coding: UTF-8 -*-
#!/Users/qwysam/.pyenv/shims/python3
import sys
import mtranslate
from mtranslate import translate
class Translator:
    def Translate(self,text,tolanguage,fromlanguage):
        return translate(text, tolanguage,fromlanguage)
class Parser:
    def Parse():
        url = sys.argv[1]
TranslatorObj = Translator()
print(TranslatorObj.Translate("Тестируем методы Питона","en",sys.argv[2]))