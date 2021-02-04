<template>
  <v-container>
    <v-row class="text-center">
      <v-col md="4" offset="4">
        <v-img
          :src="require('../assets/logo.svg')"
          class="my-3"
          contain
          height="200"
        />      
        <h1 class="display-2 font-weight-bold mb-3">
          Video Converter
        </h1>
        <v-form
          ref="form"
          
          lazy-validation
        >
          <v-file-input id="file"
            show-size
            truncate-length="15"
            label="Video Url"   
            @change="onBrowseFile"                   
          ></v-file-input>
          <v-row>
            <v-col cols="6">
              Target Quality
            </v-col>
            <v-col cols="6">
              <v-btn-toggle
              v-model="quality"              
              rounded mandatory
                >                  
                  <v-btn value="low">
                    Low
                  </v-btn>
                  <v-btn value="medium">
                    Medium
                  </v-btn>
                  <v-btn value="high">
                    High
                  </v-btn>
                </v-btn-toggle>  
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="6">
              Target Size:
            </v-col>
            <v-col cols="6">
              <v-text-field v-model="size"></v-text-field>
            </v-col>
          </v-row>
          <v-btn            
            color="success"
            class="mt-4" 
            :loading="loading"
            :disabled="loading"  
            @click="loader = 'loading'"         
          >
            Convert
          </v-btn>               
        </v-form>      
      </v-col>     
    </v-row>
  </v-container>
</template>

<script>
  import axios from 'axios'
  export default {
    name: 'Converter',

    data () {
      return {
        loader: null,
        loading: false,
        result: null,  
        previews: [],
        errorImage: "url of an image to use to indicate an error",      
      }
    },
    watch: {
      loader () {
        const l = this.loader
        this[l] = !this[l]
        const size = this.size
        const quality = this.quality        

        axios
          .post('http://localhost:5000/api/convert',
          {
            videoUrl: previews,
            targetQuality: quality,
            targetSize: size
          })
          .then(response => {
            this.result = response;
            this[l] = false;
          })
          .catch()
       
        this.loader = null
      },
      onBrowseFile(files) {
          this.previews = [];
          files.forEach((file, index) => {
              const reader = new FileReader();
              reader.addEventListener('load', e => this.previews[index] = e.target.result);
              //reader.addEventListener('error', e => this.previews[index] = this.errorImage);
              reader.readAsDataURL(file);
          });
      }      
    },
  }
</script>
