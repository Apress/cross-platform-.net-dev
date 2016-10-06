#soundex.py - Calls a .NET assembly to calculate a Soundex encoding

import sys
import CLR.Phoneticator as Phoneticator

#Display the program's output
def printresults(name, encoding):
    print "The soundex encoding of", name, "is", encoding

#The program's guts
def main():

    #The script name is always the first argument
    if len(sys.argv) <> 2: 
        print "Please pass in a surname to encode."
        sys.exit(0)
    else:
        surname = sys.argv[1] 
        printresults(surname , Phoneticator.Phoneticator.Soundex(surname))

#Only call the main method if soundex.py has not been imported
if __name__ == '__main__':
    main() 
