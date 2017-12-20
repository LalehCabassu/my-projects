//ROSTAMI HOSOORI Laleh & SOUNG Sreynoch
package uga.tpspark.flickr

import org.apache.spark.sql.types.IntegerType
import org.apache.spark.sql.types.DateType
import org.apache.spark.sql.SparkSession
import org.apache.spark.sql.types.StructType
import org.apache.spark.sql.types.ByteType
import org.apache.spark.sql.types.StructField
import org.apache.spark.sql.types.FloatType
import org.apache.spark.sql.types.StringType
import org.apache.spark.sql.types.LongType
import java.net.URLDecoder
import org.apache.spark.sql.Row
import org.apache.spark.sql.Encoders
import org.apache.spark.rdd.RDD
import org.apache.spark.SparkConf
import org.apache.spark.SparkContext
import scala.tools.nsc.transform.LambdaLift
import scala.collection.mutable.ListBuffer

object Ex2RDD {
  def main(args: Array[String]): Unit = {
    println("hello")
    val conf = new SparkConf().setAppName("Ex2RDD").setMaster("local")
    val sc = new SparkContext(conf)
    var spark: SparkSession = null
    try {
      spark = SparkSession.builder().appName("Flickr using dataframes").getOrCreate()
      val originalFlickrMeta: RDD[String] = spark.sparkContext.textFile("flickrSample.txt")
      
      // #1
      originalFlickrMeta.take(5).foreach {println}
      println(originalFlickrMeta.count())
      
      // #2
      val pictureRDD = originalFlickrMeta.map((x: String) => new Picture(x.split('\t')))
        .filter { x => x.hasValidCountry && x.hasTags }
      pictureRDD.take(5).foreach { println }
      
      // #3
      // RDD[(Country, Iterable[Picture])]
      val groupedPictureRdd:RDD[(Country, Iterable[Picture])] = pictureRDD.groupBy { x => x.c }.sortByKey()
      val oneCountry = groupedPictureRdd.take(1)(0)
      val picturesItr = oneCountry._2
      for( x <- picturesItr){
        println(x)
      }
      
      // #4
//      val countryTagRdd = groupedPictureRdd.map { x => (x._1, extractTags(x._2)) }
//      for( x <- countryTagRdd){
//        println(x)
//      }
      
      val tagRdd = pictureRDD.map { x => (x.c, x.userTags.toList) }.reduceByKey((a, b) => a ++ b)
      for( x <- tagRdd){
        println(x)
      }
      
      
      // #5
      val countryMapTagRdd = groupedPictureRdd.map { x => (x._1, mapTags(x._2)) }
      for( x <- countryMapTagRdd){
        println(x)
      }
      
      // #6
      val mapTagRdd = pictureRDD.map { x => (x.c, x.userTags.toList) }.flatMap { x => x._2.map { y => ((x._1, y), 1) } }
      .reduceByKey((a, b) => a + b).groupBy { x => x._1._1 }.map { x => (x._1, x._2.map { t => (t._1._2, t._2) }) }
      for( x <- mapTagRdd){
        println(x)
      }

    } catch {
      case e: Exception => throw e
    } finally {
      spark.stop()
    }
    println("done")
  }
  
  def extractTags(pictures:Iterable[Picture]): List[String] = {
    var result = new ListBuffer[Array[String]]()
    
    for(p <- pictures.iterator) {
       result += p.userTags 
    }

    return result.result().flatten
  }
  
  def mapTags(pictures:Iterable[Picture]): Map[String, Int] = {
    var result = Map[String, Int]()
    
    for(p <- pictures.iterator) {
      for(ut <- p.userTags) {
       if(result contains ut) {
         val v = (result get ut).get + 1
         result = result updated (ut, v)
       }
       else {
         result = result + (ut -> 1)
       }
      }
    }

    return result
  }
  
}