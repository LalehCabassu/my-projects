#__author__ = 'Laleh'

import s3handler

bucket_name = 'nbias'
folder_name = 'input'
nbias_file_full_path = 'data/usa_2012.nbi'

def main():
    print "Connecting to S3 ..."
    s3 = s3handler.S3()

    # clear nbias/input
    print "Clearing {0}/{1} ...".format(bucket_name, folder_name)
    s3.clear_folder(bucket_name, folder_name)

    # break down the nbias file into smaller files including only 1000 records each
    nbias_file = open(nbias_file_full_path, 'r')
    file_index = 0
    record_counter = 1000
    content = ''
    lines = nbias_file.readlines()
    print "Uploading input files ..."
    for line in lines:
        if record_counter == 0:
            upload_file(s3, file_index, content)

            record_counter = 1000
            content = ''
            file_index += 1
        else:
            record_counter -= 1
        content += line

    upload_file(s3, file_index, content)

    print "Done."
    nbias_file.close()


def upload_file(s3, file_index, content):
    key_name = '%(folder)s/nbias_data_%(index)05d.txt' % {"folder": folder_name, "index": file_index}
    #print "Uploading some data to {0} with key: {1}".format(bucket_name, key_name)
    s3.upload_key_from_string(bucket_name, key_name, content)

if __name__ == '__main__':
    main()