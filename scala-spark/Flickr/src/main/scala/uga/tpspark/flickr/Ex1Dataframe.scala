// ROSTAMI HOSOORI Laleh & SOUNG Sreynoch
package uga.tpspark.flickr

import org.apache.spark.SparkConf
import org.apache.spark.SparkContext
import org.apache.spark.sql.SparkSession
import org.apache.spark.sql.types.ByteType
import org.apache.spark.sql.types.FloatType
import org.apache.spark.sql.types.LongType
import org.apache.spark.sql.types.StringType
import org.apache.spark.sql.types.StructField
import org.apache.spark.sql.types.StructType
import java.util.Calendar

object Ex1Dataframe {
  def main(args: Array[String]): Unit = {
    println("hello")
    val conf = new SparkConf().setAppName("Ex1Dataframe").setMaster("local")
    val sc = new SparkContext(conf)
    
    var spark: SparkSession = null
    try {
      spark = SparkSession.builder().appName("Flickr using dataframes").getOrCreate()

      //   * Photo/video identifier
      //   * User NSID
      //   * User nickname
      //   * Date taken
      //   * Date uploaded
      //   * Capture device
      //   * Title
      //   * Description
      //   * User tags (comma-separated)
      //   * Machine tags (comma-separated)
      //   * Longitude
      //   * Latitude
      //   * Accuracy
      //   * Photo/video page URL
      //   * Photo/video download URL
      //   * License name
      //   * License URL
      //   * Photo/video server identifier
      //   * Photo/video farm identifier
      //   * Photo/video secret
      //   * Photo/video secret original
      //   * Photo/video extension original
      //   * Photos/video marker (0 = photo, 1 = video)

      val customSchemaFlickrMeta = StructType(Array(
        StructField("photo_id", LongType, true),
        StructField("user_id", StringType, true),
        StructField("user_nickname", StringType, true),
        StructField("date_taken", StringType, true),
        StructField("date_uploaded", StringType, true),
        StructField("device", StringType, true),
        StructField("title", StringType, true),
        StructField("description", StringType, true),
        StructField("user_tags", StringType, true),
        StructField("machine_tags", StringType, true),
        StructField("longitude", FloatType, false),
        StructField("latitude", FloatType, false),
        StructField("accuracy", StringType, true),
        StructField("url", StringType, true),
        StructField("download_url", StringType, true),
        StructField("license", StringType, true),
        StructField("license_url", StringType, true),
        StructField("server_id", StringType, true),
        StructField("farm_id", StringType, true),
        StructField("secret", StringType, true),
        StructField("secret_original", StringType, true),
        StructField("extension_original", StringType, true),
        StructField("marker", ByteType, true)))

      val originalFlickrMeta = spark.sqlContext.read
        .format("csv")
        .option("delimiter", "\t")
        .option("header", "false")
        .schema(customSchemaFlickrMeta)
        .load("flickrSample.txt")
        
         // #1  
      originalFlickrMeta.createOrReplaceTempView("customSchemaFlickr")
       
      val rows = spark.sql("SELECT photo_id, longitude, latitude, license" + 
                           " FROM customSchemaFlickr"+
                             " WHERE license IS NOT NULL"+ 
                             " AND longitude IS NOT NULL"+
                             " AND latitude IS NOT NULL")
      
//      rows.show(20)
      
//      #2
      val interesting_pictures = spark.sql("SELECT photo_id, longitude, latitude, license" + 
                                           " FROM customSchemaFlickr WHERE license IS NOT NULL"+
                                           " AND longitude != -1 AND latitude != -1").cache()
      // #3
       originalFlickrMeta.explain()
       rows.explain()
       interesting_pictures.explain()
       
       // #4
       interesting_pictures.show()
       
       // #5
       // * Name
       // * Attribution (0 = no, 1 = yes)
       // * NonCommercial (0 = no, 1 = yes)
       // * NonDerivative (0 = no, 1 = yes)
       // * ShareAlike (0 = no, 1 = yes)
       // * PublicDomainDedication (0 = no, 1 = yes)
       // * PublicDomainWork (0 = no, 1 = yes)

      val licenseSchemaFlickrMeta = StructType(Array(
        StructField("name", StringType, true),
        StructField("attribution", ByteType, true),
        StructField("nonCommercial", ByteType, true),
        StructField("nonDerivative", ByteType, true),
        StructField("shareAlike", ByteType, true),
        StructField("publicDomainDedication", ByteType, true),
        StructField("publicDomainWork", ByteType, true)));
        
      val licenseFlickrMeta = spark.sqlContext.read
        .format("csv")
        .option("delimiter", "\t")
        .option("header", "true")
        .schema(licenseSchemaFlickrMeta)
        .load("FlickrLicense.txt")

      licenseFlickrMeta.createOrReplaceTempView("licenseSchemaFlickr")
      val licenses = spark.sql("SELECT * FROM licenseSchemaFlickr ")
      
      interesting_pictures.createOrReplaceTempView("interestingPictures")
      val nonDerivative_interesting_pictures = 
        spark.sql("SELECT photo_id, longitude, latitude, license, licenseSchemaFlickr.nonDerivative " +
                   "FROM interestingPictures " +
                   "INNER JOIN licenseSchemaFlickr " +
                   "ON interestingPictures.license = licenseSchemaFlickr.name AND licenseSchemaFlickr.nonDerivative == 1")
      
      nonDerivative_interesting_pictures.explain()
      nonDerivative_interesting_pictures.show()

      // #7       
      val now = Calendar.getInstance().getTimeInMillis()
      val destDirName = "nonDerivativeInterestingPictures_"+now+".csv"
      
      nonDerivative_interesting_pictures.repartition(1).write.
          format("com.databricks.spark.csv").
          option("header", "true").
          option("delimiter", ",").
          save(destDirName) 

    } catch {
      case e: Exception => throw e
    } finally {
      spark.stop()
    }
    println("done")
  }
}