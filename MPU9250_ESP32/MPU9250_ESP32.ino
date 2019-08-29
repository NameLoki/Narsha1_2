#define SDA_PIN 21 
#define SCL_PIN 22
#define BT 2

#include <MPU9250_asukiaaa.h>
#include "BluetoothSerial.h"

#ifdef ESP32_HAL_I2C_H
#endif
MPU9250 mySensor;
BluetoothSerial SerialBT;
int button;

void setup() {
SerialBT.begin("Bluetooth_ESP32");
while(!Serial);
 
Serial.begin(115200);
//Serial.println("started");
 
#ifdef ESP32_HAL_I2C_H
Wire.begin(SDA_PIN, SCL_PIN); //sda, scl
#else
Wire.begin();
#endif

pinMode(BT,INPUT);
mySensor.setWire(&Wire); 
mySensor.beginAccel();
mySensor.beginGyro();
 
// 센서 고정
//mySensor.magXOffset = -50;
//mySensor.magYOffset = -55;
//mySensor.magZOffset = -10;
}
 
void loop() { 
 delay(100);
mySensor.accelUpdate();
mySensor.gyroUpdate();
/*
//Serial.println("print accel values");
Serial.println("accelX: " + String(mySensor.accelX()));
Serial.println("accelY: " + String(mySensor.accelY()));
Serial.println("accelZ: " + String(mySensor.accelZ()));
Serial.println("accelSqrt: " + String(mySensor.accelSqrt()));
*/
//if (Serial.available()){
//double _tmp[6] = {mySensor.accelX(), mySensor.accelY(), mySensor.accelZ(), (double) mySensor.magX(), (double) mySensor.magY(), (double) mySensor.magZ()};
//uint8_t _tmp2[48];

//for(int i = 0; i < 47; i++)
//{
//  uint8_t _tmp3 = 0xFF;
//}


//SerialBT.println(1);
//SerialBT.print("Accel : X  = ");
button = digitalRead(BT);
SerialBT.println((String)button+" "+(String)(mySensor.accelX())+" "+(String)(mySensor.accelY())+" "+(String)(mySensor.accelZ())); //3축 가속도 센서
//SerialBT.print(" / Y = ");
//SerialBT.printf("%f ",mySensor.accelY());
//SerialBT.print(" / Z = ");
//SerialBT.println(mySensor.accelZ());
//SerialBT.print("Gyro : X = ");
//SerialBT.print(+mySensor.gyroX()); //중력
//SerialBT.print(" / Y = ");
//SerialBT.print(mySensor.gyroY());
//SerialBT.print(" / Z = ");
//SerialBT.println(mySensor.gyroZ());
//SerialBT.println();


//}
//SerialBT.write();
/*
//Serial.println("print mag values");
Serial.println("magX: " + String(mySensor.magX()));
Serial.println("maxY: " + String(mySensor.magY()));
Serial.println("magZ: " + String(mySensor.magZ())); 
Serial.println("horizontal direction: " + String(mySensor.magHorizDirection()));
 
Serial.println("at " + String(millis()) + "ms");
*/
//SerialBT.write(mySensor.magX()); //중력
//SerialBT.write(mySensor.magY());
//SerialBT.write(mySensor.magZ());
//SerialBT.write();
//delay(500);
}
