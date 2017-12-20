#!/usr/bin/python
import sys

def main(argv):
  # line = sys.stdin.readline()
  # try:
  #   while line:
  #     line = line.rstrip()
  #     words = line.split()
  #     for word in words:
  #       print "LongValueSum:" + word + "\t" + "1"
  #     line = sys.stdin.readline()
  # except "end of file":
  #   return None

  for line in sys.stdin:
      line = line.rstrip()
      words = line.split()
      for word in words:
          print "LongValueSum:" + word + "\t" + "1"

if __name__ == "__main__":
  main(sys.argv)