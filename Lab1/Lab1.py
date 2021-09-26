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
parser = Parser()
str_ru = parser.FindData(parser.ReadHTML(sys.argv[1]))
print("Parsing Complete")
str_ru_size = len(str_ru)
chars_to_remove = str_ru_size-3000
if(str_ru_size>3000):
    str_ru = str_ru[:str_ru_size-chars_to_remove]
file_ru = open("/Users/qwysam/repos/Information_Theory/Information-Theory/Lab1/rus.txt","w")
file_ru.write(str_ru)
file_ru.close()
print("rus.txt ready")
str_ru_size = len(str_ru)
TranslatorObj = Translator()
str_en = TranslatorObj.Translate(str_ru,"en",sys.argv[2])
file_en = open("/Users/qwysam/repos/Information_Theory/Information-Theory/Lab1/eng.txt","w")
file_en.write(str_en)
file_en.close()
print("eng.txt ready")
str_uk = TranslatorObj.Translate(str_ru,"uk",sys.argv[2])
file_uk = open("/Users/qwysam/repos/Information_Theory/Information-Theory/Lab1/ukr.txt","w")
file_uk.write(str_uk)
file_uk.close()
print("ukr.txt ready")
