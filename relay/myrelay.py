#!/usr/bin/python
import RPi.GPIO as GPIO
import time

GPIO.setmode(GPIO.BCM)

pinList = [17]

for i in pinList:
	GPIO.setup(i, GPIO.OUT)
	GPIO.output(i, GPIO.HIGH)
	print "GPIO 17 is HIGH"
SleepTimeL = 20

try:
	GPIO.output(17, GPIO.LOW)
	print "GPIO 17 is LOW"
	time.sleep(SleepTimeL)
	GPIO.cleanup()	
except KeyboardInterrupt:
	print "Quit"

	GPIO.cleanup()

