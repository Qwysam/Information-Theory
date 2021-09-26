# -*- coding: UTF-8 -*-
#!/Users/qwysam/.pyenv/shims/python3
import sys
from bs4 import BeautifulSoup
import urllib.request
from mtranslate import translate
from html.parser import HTMLParser
class Translator:
    def Translate(self,text,tolanguage,fromlanguage):
        return translate(text, tolanguage,fromlanguage)
class Parser:
    def ReadHTML(self,url):
        fp = urllib.request.urlopen(url)
        mybytes = fp.read()
        mystr = mybytes.decode("utf8")
        fp.close()
        return mystr
    def FindData(self,text):
        soup = BeautifulSoup(text, 'lxml')
        res = ''
        for tag in soup.find_all("p"):
            res+=tag.text
            res+='\n'
        return res
TranslatorObj = Translator()
#print(TranslatorObj.Translate("Тестируем методы Питона","en",sys.argv[2]))
parser = Parser()
print(parser.FindData(parser.ReadHTML("https://masterimargo.ru/book-1.html")))
