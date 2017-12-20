#__author__ = 'Laleh'

import boto
from boto.s3.key import Key


class S3:
    s3 = boto.connect_s3()

    def get_bucket(self, bucket_name):
        return self.s3.get_bucket(bucket_name)

    def upload_key_from_string(self, bucket_name, key_name, content):
        bucket = self.get_bucket(bucket_name)
        k = Key(bucket)
        k.key = key_name
        k.set_contents_from_string(content)

    def clear_folder(self, bucket_name, folder_name):
        bucket = self.get_bucket(bucket_name)
        keys = bucket.list(folder_name)
        for key in keys:
            key.delete()

    def clear_bucket(self, bucket_name):
        bucket = self.get_bucket(bucket_name)
        for key in bucket.list():
            key.delete()

    def delete_bucket(self, bucket_name):
        self.clear_bucket(bucket_name)
        self.s3.delete_bucket(bucket_name)
