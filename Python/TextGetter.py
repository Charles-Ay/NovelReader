###TEXTGETTERV1

import os
from os import listdir
from os.path import join, isdir, dirname, basename

import requests
from bs4 import BeautifulSoup
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.keys import Keys
import time

filepath = dirname(__file__)


def read_input():
    list_of_results = []

    # Get input from file
    with open(filepath + "//input.txt", "r") as read_obj:
        # Read all lines in the file one by one
        for line in read_obj:
            # For each line, check if line contains the string
            list_of_results.append((line.rstrip()))

    for row in list_of_results:
        print(row)

    url = list_of_results[0]

    # initiating the webdriver. Parameter includes the path of the webdriver.
    ser = Service("./chromedriver")
    op = webdriver.ChromeOptions()
    driver = webdriver.Chrome(service=ser, options=op)
    driver.get(url)

    # this is just to ensure that the page is loaded
    time.sleep(3)

    html = driver.page_source

    # Now, we could simply apply bs4 to html variable
    soup = BeautifulSoup(html, "html.parser")
    driver.close()

    return soup, driver


def get_file(soup):
    novel_title = ""
    # get novel title
    for title in soup.find_all('title'):
        novel_title = title.get_text()

    if os.path.exists(filepath + "/books/" + novel_title + ".txt"):
        text_file = open(filepath + "/books/" + novel_title + ".txt", "w+", encoding="utf-8")
        return text_file
    else:
        with open("books/" + novel_title + ".txt", "x") as text_file:
            text_file.write("")
            text_file.close()

        text_file = open(filepath + "/books/" + novel_title + ".txt", "w+", encoding="utf-8")
        return text_file


def parse(text):
    for num, line in enumerate(text, 1):
        if "\n" in line[0]:
            text = text.split("\n", 1)[1]
        else:
            break

    num = 0
    cur = text.split('\n')
    tmp = cur[0]

    while num != 10000:
        if "ShareNextStay" in cur[0]:
            text = text.split("\n", 1)[1]
            break
        else:
            text = text.split("\n", 1)[1]
            cur.pop(0)
        ++num

    return tmp + "\n" + text


def output(soup, text_file):
    # Write to file

    text = parse(soup.text)

    text_file.write(text)

    # Clean up
    text_file.close()


def main():
    soup, driver = read_input()
    text_file = get_file(soup=soup)
    output(soup=soup, text_file=text_file)


if __name__ == "__main__":
    main()
